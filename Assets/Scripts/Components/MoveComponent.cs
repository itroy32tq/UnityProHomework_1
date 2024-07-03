using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent
    {
        
        public void Move(Rigidbody2D rigidbody, Vector2 vector, float speed)
        {
            var nextPosition = rigidbody.position + vector * speed;
            rigidbody.MovePosition(nextPosition);
        }

    }
}