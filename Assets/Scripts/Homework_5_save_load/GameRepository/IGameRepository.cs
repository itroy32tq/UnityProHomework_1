namespace Assets.Scripts
{
    public interface IGameRepository
    {
        void SetData<T>(T data);
        bool TryGetData<T>(out T data);
        void SaveGameState();
        void LoadGameState();
    }
}
