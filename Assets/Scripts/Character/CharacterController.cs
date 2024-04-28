using Assets.Scripts.Interface;
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
        private void Construct(Character character, GameManager gameManager)
        { 
            _character = character; _gameManager = gameManager;
        }

        private void CharacterDeathHandler(Character _)
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