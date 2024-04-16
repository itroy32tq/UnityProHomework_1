using Assets.Scripts;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners= new();

        [SerializeField, ReadOnly] private GameState _gameState;

        private void Awake()
        {
            _gameState = GameState.Off;

            IGameListener.OnRegister += AddListener;
        }
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
            IGameListener.OnRegister -= AddListener;
        }
        private void AddListener(IGameListener _) => _gameListeners.Add(_);

        [Button]
        public void StartGame()
        {
            var startListeners = _gameListeners?
                .GetItemsOfType<IGameStartListener>().ForEach(x=> x.OnStartGame());

            _gameState = GameState.Start;

        }
        [Button]
        public void FinishGame()
        {
            var finishListeners = _gameListeners?
                .GetItemsOfType<IGameFinishListener>().ForEach(x => x.OnFinishGame());

            _gameState = GameState.Finish;

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        [Button]
        public void PauseGame()
        {
            var pauseListeners = _gameListeners?
                .GetItemsOfType<IGamePauseListener>().ForEach(x => x.OnPauseGame());
            _gameState = GameState.Pause;

        }

        [Button]
        public void ResumeGame()
        {
            var resumeListeners = _gameListeners?
                .GetItemsOfType<IGameResumeListener>().ForEach(x => x.OnResumeGame());
            _gameState = GameState.Resume;
        }
    }
}