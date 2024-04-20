using Assets.Scripts;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        private readonly HashSet<IGameFixedUpdateListener> _gameFixedUpdateListeners= new();

        [SerializeField, ReadOnly] private GameState _gameState;
        [SerializeField] Button _startButton;
        [SerializeField] Button _pauseButton;
        [SerializeField] TMP_Text _text;
        private void Awake()
        {
            _gameState = GameState.Off;

            IGameListener.OnRegister += AddListener;
            _startButton.onClick.AddListener(OnStartButtonClick);
            _pauseButton.onClick.AddListener(PauseGame);
            
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
           
            var startListeners = _gameListeners.ToList()?
                .OfType<IGameStartListener>().ForEach(x=> x.OnStartGame());

            _gameUpdateListeners.AddRange(_gameListeners.OfType<IGameUpdateListener>().ToList());
            _gameFixedUpdateListeners.AddRange(_gameListeners.OfType<IGameFixedUpdateListener>().ToList());

            _gameState = GameState.Start;

        }
        public void OnStartButtonClick()
        {
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
            var finishListeners = _gameListeners?
                .OfType<IGameFinishListener>().ForEach(x => x.OnFinishGame());

            _gameState = GameState.Finish;

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
        private void Update()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }
            float deltaTime = Time.deltaTime;
            List<IGameUpdateListener> listeners = _gameUpdateListeners.ToList();
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnUpdate(deltaTime);
            }

        }
        private void FixedUpdate()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }

            float fixedDeltaTime = Time.fixedDeltaTime;
            List<IGameFixedUpdateListener> listeners = _gameFixedUpdateListeners.ToList();
            for (int i = 0; i < _gameFixedUpdateListeners.ToList().Count; i++)
            {
                listeners[i].OnFixedUpdate(fixedDeltaTime);
            }

        }
        [Button]
        private void PauseGame()
        {
            var pauseListeners = _gameListeners?
                .OfType<IGamePauseListener>().ForEach(x => x.OnPauseGame());
            if (_gameState != GameState.Pause)
            {
                _gameState = GameState.Pause;
            }
            else
            {
                _gameState = GameState.Start;
            }
            

        }

        [Button]
        private void ResumeGame()
        {
            var resumeListeners = _gameListeners?
                .OfType<IGameResumeListener>().ForEach(x => x.OnResumeGame());
            _gameState = GameState.Start;
        }
    }
}