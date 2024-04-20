namespace Assets.Scripts.Interface
{
    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
}
