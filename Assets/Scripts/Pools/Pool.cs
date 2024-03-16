using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenericPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _items;
        private readonly T _pref;
        public Pool(T pref, int size)
        {
            _items = new Queue<T>(size);
            for (int i = 0; i < size; i++)
            {
                _items.Enqueue(Object.Instantiate(pref));
            }
        }
        public T Get()
        { 
            return _items.Dequeue();
        }
        public T TryGet(Transform transform)
        {
            if (_items.TryDequeue(out T item))
            {
                item.transform.SetParent(transform);
                return item;
            }
            else
            {
                T newItem = Object.Instantiate(_pref);
                newItem.transform.SetParent(transform);
                Debug.LogWarning(" Видимо это нештатная ситуация, если объекты в пуле кончились ");
                return newItem;
            }

        }
        public void Release(T item) 
        {
            _items.Enqueue(item);
        }
    }
}
