using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Character _character;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        private void CharacterDeathHandler(Character _) => _gameManager.FinishGame();

        public void OnStartGame()
        {
            _character.OnCharacterDie += CharacterDeathHandler;
        }

        public void OnFinishGame()
        {
            _character.OnCharacterDie -= CharacterDeathHandler;
        }
    }
}