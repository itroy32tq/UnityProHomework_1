using Assets.Scripts.Interface;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        private float _horizontalDirection;
        public float HorizontalDirection => _horizontalDirection;

        [SerializeField] private Unit unit;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                unit.Shoot();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _horizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _horizontalDirection = 1;
            }
            else
            {
                _horizontalDirection = 0;
            }
        }
        
        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(_horizontalDirection, 0) * Time.fixedDeltaTime;
            unit.Move(direction);
        }
    }
}