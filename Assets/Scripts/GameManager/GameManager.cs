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
        [SerializeField, ReadOnly] private GameState _gameState;

        private void Awake()
        {
            _gameState = GameState.Off;

            IGameListener.onRegister += AddListener;
        }
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
            IGameListener.onRegister -= AddListener;
        }
        private void AddListener(IGameListener _) => _gameListeners.Add(_);

        [Button]
        public void StartGame()
        {
            var startListener = _gameListeners?
                .GetUniqueItemOfType<IGameStartListener>();
            startListener.OnStartGame();
            _gameState = GameState.Start;

        }
        [Button]
        public void FinishGame()
        {
            var finishListener = _gameListeners?
                .GetUniqueItemOfType<IGameFinishListener>();
            finishListener.OnFinishGame();

            _gameState = GameState.Finish;

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        [Button]
        public void PauseGame()
        {
            var pauseListener = _gameListeners?
                .GetUniqueItemOfType<IGamePauseListener>();
            pauseListener.OnPauseGame();
            _gameState = GameState.Pause;

        }

        [Button]
        public void ResumeGame()
        {
            var resumeeListener = _gameListeners?
                .GetUniqueItemOfType<IGamePauseListener>();
            resumeeListener.OnPauseGame();
            _gameState = GameState.Resume;

        }

    }
}