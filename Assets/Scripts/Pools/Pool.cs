using Assets.Scripts.Inventary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenericPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _items;

        public IFactory<T> Factory { get; private set; }
        public bool AutoExpand { get; set; }
        public Pool(int size, IFactory<T> factory)
        {
            _items = new Queue<T>(size);
            Factory = factory;

            for (int i = 0; i < size; i++)
            {
                T item = Factory.Create();
                item.gameObject.SetActive(false);
                _items.Enqueue(item);
            }
        }
        public T Get()
        { 
            return _items.Dequeue();
        }
        public bool HasFreeElement(out T element)
        {
            element = _items?
                .FirstOrDefault(x => !x.isActiveAndEnabled);
            element?.gameObject.SetActive(true);
            return element != null;
        }
        public T TryGet()
        {
            if (HasFreeElement(out T element))
            {
                return element;
            }
            if (AutoExpand)
            {
                T newItem = Factory.Create();
                newItem?.gameObject.SetActive(true);
                return newItem;
            }
            else
            {
                return null;
                throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
            } 
        }
        public void Release(T item) 
        {
            item.gameObject.SetActive(false);
        }
    }
}
