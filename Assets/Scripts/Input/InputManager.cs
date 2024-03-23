using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private CharacterController characterController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterController.FireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
        
        private void FixedUpdate()
        {
            character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(new Vector2(this.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}