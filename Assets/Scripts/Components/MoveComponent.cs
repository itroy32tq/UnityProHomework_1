using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent
    {
        private Rigidbody2D _rigidbody2D;
        private float _speed;
        
        public void Move(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void Construct(Rigidbody2D rigidbody2D, float speed)
        { 
            _rigidbody2D = rigidbody2D; _speed = speed;
        }
    }
}