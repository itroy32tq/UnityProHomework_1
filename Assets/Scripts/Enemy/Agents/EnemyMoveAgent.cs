using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private bool _isReached;
        public bool IsReached => _isReached;
        private Vector2 _direction;
        public Vector2 Direction => _direction;

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
            {
                return;
            }
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }
            _direction = vector.normalized;
            var moveDirection = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(moveDirection);
        }
    }
}