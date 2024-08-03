using System;

namespace Assets.Scripts.Interface
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;

        public static void Register(IGameListener listener)
        {
            if (null != OnRegister)
            {
                OnRegister?.Invoke(listener);
            }
        }
    }
}
