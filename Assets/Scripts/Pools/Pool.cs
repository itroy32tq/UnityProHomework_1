using System.Collections.Generic;

namespace Assets.Scripts.GenericPool
{
    public class Pool<T> where T : UnityEngine.Object
    {
        private readonly Queue<T> _items;
        public Pool(T pref, int size)
        {
            _items = new Queue<T>(size);
            for (int i = 0; i < size; i++)
            {
                _items.Enqueue(UnityEngine.Object.Instantiate(pref));
            }
        }
        public T Get()
        { 
            return _items.Dequeue();
        }
        public void Release(T item) 
        {
            _items.Enqueue(item);
        }
    }
}
