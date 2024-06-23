using UnityEngine;

namespace Assets.Scripts.Inventary
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
