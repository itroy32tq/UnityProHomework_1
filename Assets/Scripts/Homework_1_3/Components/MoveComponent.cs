using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent
    {
        
        public void Move(Rigidbody2D rigidbody, Vector2 vector, float speed)
        {
            Vector2 nextPosition = rigidbody.position + vector * speed;
            rigidbody.MovePosition(nextPosition);
        }

    }
}