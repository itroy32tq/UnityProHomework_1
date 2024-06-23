using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent
    {
        private Vector2 _destination = new() { x = 0.04f, y = 3.74f };
        public bool IsReached { get; private set; }
        public Vector2 Direction { get; private set; }

        public event Action<Vector2> OnMove;

        public void Tick(float fixedDeltaTime, Vector2 unitPosition)
        {
            if (IsReached)
            {
                return;
            }

            var vector = _destination - unitPosition;
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