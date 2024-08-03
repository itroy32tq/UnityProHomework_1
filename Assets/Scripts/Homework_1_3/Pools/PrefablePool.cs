using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Pools
{
    public sealed class PrefablePool<T> where T : class, IPrefable
    {
        private readonly Queue<T> _items;
        private readonly IFactory<T> _factory;

        public PrefablePool(int size, IFactory<T> factory)
        {
            _items = new Queue<T>(size);
            _factory = factory;

            for (int i = 0; i < size; i++)
            {
                T item = _factory.Create();
                item.Prefab.SetActive(false);
                _items.Enqueue(item);
            }
        }

        private bool HasFreeElement(out T element)
        {
            
            element = _items?
                .FirstOrDefault(x => !x.Prefab.activeInHierarchy);
            element?.Prefab.SetActive(true);
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
            item.Prefab.SetActive(false);
        }
    }
}
