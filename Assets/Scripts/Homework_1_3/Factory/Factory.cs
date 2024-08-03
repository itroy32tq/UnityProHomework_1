using Assets.Scripts.Inventary;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public sealed class Factory<T> : IFactory<T> where T : MonoBehaviour
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
            return Object.Instantiate(_prefab, _container);
        }
    }
}
