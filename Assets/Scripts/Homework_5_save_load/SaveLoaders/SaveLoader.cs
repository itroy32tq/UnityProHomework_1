namespace Assets.Scripts
{
    public abstract class SaveLoader<TService, TData> : ISaveLoader
    {
        private readonly TService _service;
        private readonly IGameRepository _gameRepository;

        public SaveLoader(TService service, IGameRepository gameRepository)
        {
            _service = service;
            _gameRepository = gameRepository;
        }

        public void Save()
        {
            TData data = ConvertToData(_service);
            _gameRepository.SetData(data);
        }

        public void Load()
        {
            if (_gameRepository.TryGetData(out TData data))
            {
                SetupData(_service, data);
            }
            else 
            {
                SetupDefaultData(_service);
            }
        }

        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TService service, TData data);
        protected virtual void SetupDefaultData(TService service) { }
    }
}
