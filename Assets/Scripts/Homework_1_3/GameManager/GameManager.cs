using Assets.Scripts;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly HashSet<IGameListener> _gameListeners = new();
        private readonly HashSet<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly HashSet<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        [SerializeField] private GameState _gameState = GameState.Off;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private DiContainer[] _diContainers;

        private void Awake()
        {
            _gameState = GameState.Off;

            foreach (DiContainer container in _diContainers)
            {
                foreach (IGameListener listner in container.GameListeners)
                {
                    AddListner(listner);
                }
            }

            _startButton.onClick.AddListener(OnStartButtonClick);
            _pauseButton.onClick.AddListener(PauseGame);
            
        }
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
        }

        public void AddListner(IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                _gameUpdateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(fixedUpdateListener);
            }
        }
      
        [Button]
        public void StartGame()
        {

            foreach (IGameStartListener listener in _gameListeners.OfType<IGameStartListener>())
            {
                listener.OnStartGame();
            }
            _gameState = GameState.Start;
        }

        private void OnStartButtonClick()
        {
            _startButton.gameObject.SetActive(false);
            StartCoroutine(ExecuteAfterTime(1));
        }

        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            _text.text = "3";
            yield return new WaitForSeconds(timeInSec);
            _text.text = "2";
            yield return new WaitForSeconds(timeInSec);
            _text.text = "1";
            yield return new WaitForSeconds(timeInSec);

            StartGame();

            _text.gameObject.SetActive(false);
        }

        [Button]
        public void FinishGame()
        {

            foreach (IGameListener listener in _gameListeners)
            {
                if (listener is IGameFinishListener finishListener)
                { 
                    finishListener.OnFinishGame();
                }
            }

            _gameState = GameState.Finish;
            _text.gameObject.SetActive(true);
            _text.text = "Game over!";
            Time.timeScale = 0;
        }

        private void Update()
        {
            if (_gameState == GameState.Off || _gameState == GameState.Pause)
            {
                return;
            }


            float deltaTime = Time.deltaTime;

            foreach (IGameUpdateListener listener in _gameUpdateListeners)
            {
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_gameState == GameState.Off || _gameState == GameState.Pause)
            {
                return;
            }

            float fixedDeltaTime = Time.fixedDeltaTime;

            foreach (IGameFixedUpdateListener listener in _gameFixedUpdateListeners)
            {
                listener.OnFixedUpdate(fixedDeltaTime);
            }
        }

        [Button]
        private void PauseGame()
        {
            if (_gameState != GameState.Pause)
            {
                foreach (IGameListener listener in _gameListeners)
                {
                    if (listener is IGamePauseListener pauseListener)
                    {
                        pauseListener.OnPauseGame();
                    }
                }

                _gameState = GameState.Pause;
                return;
            }

            if (_gameState == GameState.Pause)
            {
                foreach (IGameListener listener in _gameListeners)
                {
                    if (listener is IGameResumeListener resumeListener)
                    {
                        resumeListener.OnResumeGame();
                    }
                }

                _gameState = GameState.Resume;
            }
        }

        [Button]
        private void ResumeGame()
        {
            
            foreach (IGameListener listener in _gameListeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            _gameState = GameState.Resume;
        }
    }
}