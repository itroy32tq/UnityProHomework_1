using System;

namespace Assets.Scripts
{
    public interface IStorageService
    {
        void Save(string key, object data);
        void Load<T>(string key, Action<T> action);
    }
}
