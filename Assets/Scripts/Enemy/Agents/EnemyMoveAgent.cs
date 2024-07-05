using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent
    {
        
        public Vector2 Direction { get; private set; }
        public event Action<Vector2> OnMove;

        public void Tick(Enemy enemy, float fixedDeltaTime, Vector2 unitPosition)
        {
            if (enemy.IsReached)
            {
                return;
            }

            Vector2 vector = enemy.Destination - unitPosition;

            if (vector.magnitude <= 0.25f)
            {
                enemy.SetReachedState(true);
                return;
            }

            Direction = vector.normalized;
            Vector2 moveDirection = vector.normalized * fixedDeltaTime;
            OnMove?.Invoke(moveDirection);
        }
    }
}