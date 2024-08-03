using Newtonsoft.Json;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<int, string> _gameStateDict = new();
        private const string KEY = "gameState.json";
        private readonly IStorageService _storageService = new StorageService();

        public void SetData<T>(T data)
        {
            int key = GetKey<T>();

            string value = JsonConvert.SerializeObject(data, typeof(T), Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            _gameStateDict[key] = value;

        }

        private int GetKey<T>()
        {
            return typeof(T).FullName.GetHashCode();
        }

        public void SaveGameState()
        {
            _storageService.Save(KEY, _gameStateDict);
        }

        public bool TryGetData<T>(out T data)
        {

            var key = GetKey<T>();

            if (_gameStateDict.TryGetValue(key, out string value))
            { 
                data = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings 
                { 
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                });
                return true;
            }

            data = default;
            return false;
        }

        public void LoadGameState()
        {
            _storageService.Load<Dictionary<int, string>>(KEY, data => 
            { 
                _gameStateDict = data; 
            });
            
        }
    }
}
