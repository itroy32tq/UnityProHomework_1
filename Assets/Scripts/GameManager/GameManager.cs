using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        public void OnEnable()
        {
            _characterController.OnCharacterDieEvent += FinishGame;
        }
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}