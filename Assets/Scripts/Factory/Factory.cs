using Assets.Scripts.Inventary;
using ShootEmUp;
using System;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class Factory<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        public Factory(T prefab, Transform transform)
        {
            _prefab = prefab;
            _container = transform;
        }
        public T Create()
        {
            return UnityEngine.Object.Instantiate(_prefab, _container);
        }
    }
}
