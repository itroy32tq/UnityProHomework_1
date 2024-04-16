using Assets.Scripts.Inventary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenericPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _items;
        private readonly IFactory<T> _factory;

        public Pool(int size, IFactory<T> factory)
        {
            _items = new Queue<T>(size);
            _factory = factory;

            for (int i = 0; i < size; i++)
            {
                T item = _factory.Create();
                item.gameObject.SetActive(false);
                _items.Enqueue(item);
            }
        }

        private bool HasFreeElement(out T element)
        {
            element = _items?
                .FirstOrDefault(x => !x.isActiveAndEnabled);
            element?.gameObject.SetActive(true);
            return element != null;
        }

        public bool TryGet(out T elem)
        {
            if (HasFreeElement(out T element))
            {
                elem = element;
                return true;
            }
            else
            {
                elem = null;
                return false;
            } 
        }

        public void Release(T item) 
        {
            item.gameObject.SetActive(false);
        }
    }
}
