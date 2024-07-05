using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDethObserver : IGameStartListener, IGameFinishListener
    {
        private Character _character;
        private GameManager _gameManager;

        [Inject]
        public void Construct(Character character, GameManager gameManager)
        { 
            _character = character; 
            _gameManager = gameManager;
        }

        private void CharacterDeathHandler()
        {
            _gameManager.FinishGame();
        }

        public void OnStartGame()
        {
            _character.OnCharacterDieingHandler += CharacterDeathHandler;
        }

        public void OnFinishGame()
        {
            _character.OnCharacterDieingHandler -= CharacterDeathHandler;
        }
    }
}