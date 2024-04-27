using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        private Vector2 _destination;
        
        public bool IsReached { get; private set; }
        public Vector2 Direction { get; private set; }
        public event Action<Vector2> OnMove;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (IsReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }
            Direction = vector.normalized;
            var moveDirection = vector.normalized * fixedDeltaTime;
            OnMove?.Invoke(moveDirection);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }
    }
}