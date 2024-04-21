using Assets.Scripts.InfroStructure;
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

        public void Install()
        {
            _installers.ForEach(x => x.Install(this));
        }

        public void Resolve()
        {
            var rootGameObjects = gameObject.scene.GetRootGameObjects()?.ForEach(x => InjectGameObject(x.transform));
        }

        private void Awake()
        {
            if (_installOnAwake)
            {
                Install();
            }
        }

        private void Start()
        {
            if (_resolveOnStart)
            {
                Resolve();
            }
        }

        public object GetService(Type contractType)
        {
            return _services[contractType];
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

        private void Inject(MonoBehaviour component)
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
                    var parameterType = parametersInfo.ParameterType;
                    object service = _services[parameterType];
                    args[i] = service;
                }

                method.Invoke(component, args);
            }

        }
    }
}
