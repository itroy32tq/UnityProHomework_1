using Assets.Scripts;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using Sirenix.OdinInspector;
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
                .GetItemsOfType<IGameStartListener>();

            foreach (var listener in startListeners) 
            {
                listener.OnStartGame();
            }

            _gameState = GameState.Start;

        }
        [Button]
        public void FinishGame()
        {
            var finishListeners = _gameListeners?
                .GetItemsOfType<IGameFinishListener>();

            foreach (var listener in finishListeners)
            {
                listener.OnFinishGame();
            }

            _gameState = GameState.Finish;

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        [Button]
        public void PauseGame()
        {
            var pauseListeners = _gameListeners?
                .GetItemsOfType<IGamePauseListener>();

            foreach (var listener in pauseListeners)
            {
                listener.OnPauseGame();
            }

            
            _gameState = GameState.Pause;

        }

        [Button]
        public void ResumeGame()
        {
            var resumeListeners = _gameListeners?
                .GetItemsOfType<IGameResumeListener>();

            foreach (var listener in resumeListeners)
            {
                listener.OnResumeGame();
            }
            _gameState = GameState.Resume;

        }

    }
}