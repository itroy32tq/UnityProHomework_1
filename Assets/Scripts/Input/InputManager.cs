using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameUpdateListener, IGameFixedUpdateListener
    {
        private float _horizontalDirection;
        public float HorizontalDirection => _horizontalDirection;

        public event Action OnShoot;
        public event Action<Vector2> OnMove;

        private void Awake()
        {
            IGameListener.Register(this);
        }
        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShoot?.Invoke();
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
            Vector2 direction = new Vector2(_horizontalDirection, 0) * fixedDeltaTime;
            OnMove?.Invoke(direction);
        }
    }
}