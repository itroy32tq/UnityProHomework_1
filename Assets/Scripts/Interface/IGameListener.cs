using System;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public interface IGameListener
    {
        public static event Action<IGameListener> onRegister;

        public static void Register(IGameListener listener)
        {
            
            onRegister.Invoke(listener);
        }
    }
}
