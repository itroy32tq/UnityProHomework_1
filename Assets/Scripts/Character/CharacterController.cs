using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {

        [SerializeField] Character _character;
        [SerializeField] GameManager _gameManager;

        private void OnEnable()
        {
            _character.OnCharacterDie += CharacterDeathHandler;
        }

        private void OnDisable()
        {
            _character.OnCharacterDie -= CharacterDeathHandler;
            
        }

        private void CharacterDeathHandler(Character _) => _gameManager.FinishGame();

    }
}