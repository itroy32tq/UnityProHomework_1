using UnityEngine;

namespace Assets.Scripts.Inventary
{
    public interface IFactory<T>
    {
        T Create();
    }
}
