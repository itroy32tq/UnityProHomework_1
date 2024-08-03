using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : IGameUpdateListener, IGameFixedUpdateListener
    {
        private float _horizontalDirection;

        public event Action OnInputShootingHandler;
        public event Action<Vector2> OnInputMovingHandler;

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnInputShootingHandler?.Invoke();
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

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_horizontalDirection == 0)
                return;

            Vector2 direction = new Vector2(_horizontalDirection, 0) * fixedDeltaTime;
            OnInputMovingHandler?.Invoke(direction);
        }
    }
}