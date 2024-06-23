using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class DiContainer : MonoBehaviour
    {
        private readonly Dictionary<Type, object> _services = new();

        [field: SerializeField] private bool _installOnAwake;
        [field: SerializeField] private bool _resolveOnStart;
        [field: SerializeField] private Installer[] _installers;
        public List<IGameListener> GameListeners { get; private set; } = new List<IGameListener>();

        private void Awake()
        {
            if (_installOnAwake)
            {
                
                Install();
                FindGameListeners();
            }
        }

        private void FindGameListeners()
        {
            foreach (var installer in _installers)
            {
                FindGameListener(installer);
            }
        }

        private void FindGameListener(Installer installer)
        {

            Type type = installer.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(ListenerAttribute)) && field.GetValue(installer) is IGameListener gameListener)
                {
                    GameListeners.Add(gameListener);
                }
            }
        }

        private void Start()
        {
            if (_resolveOnStart)
            {
                Resolve();
            }
        }

        public void Install()
        {
            _installers.ForEach(x => x.Install(this));
        }

        public void Resolve()
        {
            //инжект в поля инсталлеров
            foreach (var module in _installers)
            {
                FieldInfo[] fields = module.GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
                );

                foreach (var field in fields)
                {
                    var target = field.GetValue(module);
                    Inject(target);
                }
            }
            //инжект в объекты на сцене
            var rootGameObjects = gameObject.scene.GetRootGameObjects()
                .ForEach(x => InjectGameObject(x.transform));
        }

        public object GetService(Type contractType)
        {
            return _services[contractType];
        }

        public bool TryGetService(Type contractType, out object service)
        {
            if (_services.TryGetValue(contractType, out service))
            {
                return true;
            }
            return service == null;
        }

        public T GetService<T>() where T : class
        {
            return _services[typeof(T)] as T;
        }

        public void AddService<T>(object service)
        {
            _services[typeof(T)] = service;
        }

        public void RemoveService<T>()
        {
            _services.Remove(typeof(T));
        }

        public void Clear()
        {
            _services.Clear();
        }

        private void InjectGameObject(Transform transform)
        {
            transform.GetComponents<MonoBehaviour>().ForEach(x => Inject(x));

            foreach (Transform child in transform)
            {
                InjectGameObject(child);
            }
        }

        private void Inject(object component)
        {
            Type type = component.GetType();
            MethodInfo[] methods = type.GetMethods
                (
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.FlattenHierarchy 
                );

            foreach (MethodInfo method in methods ) 
            {
                if (!method.IsDefined(typeof(InjectAttribute)))
                {
                    continue;
                }

                ParameterInfo[] parametrs = method.GetParameters();
                object[] args = new object[parametrs.Length];

                for (int i = 0; i < parametrs.Length; i++)
                {
                    ParameterInfo parametersInfo = parametrs[i];
                    Type parameterType = parametersInfo.ParameterType;

                    if (TryGetService(parameterType, out object result))
                    {
                        args[i] = result;
                        continue;
                    }
                    else
                    {
                        var parent = gameObject.GetComponentInParent<DiContainer>();
                        if (parent != null)
                        {
                            if (parent.TryGetService(parameterType, out result))
                            {
                                args[i] = result;
                                continue;
                            }
                        }

                        throw new Exception($"failed to find a dependency for the type {nameof(parameterType)} ");
                    }
                    
                }

                method.Invoke(component, args);
            }

        }
    }
}
