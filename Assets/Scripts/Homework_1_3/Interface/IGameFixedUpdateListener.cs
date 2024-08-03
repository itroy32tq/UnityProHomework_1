namespace Assets.Scripts.Interface
{
    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}
