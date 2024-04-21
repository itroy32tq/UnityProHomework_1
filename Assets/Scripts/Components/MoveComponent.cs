using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speed = 5.0f;
        
        public void Move(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
        private void Construct(Rigidbody2D rigidbody2D, float speed)
        { 
            _rigidbody2D = rigidbody2D; _speed = speed;
        }
    }
}