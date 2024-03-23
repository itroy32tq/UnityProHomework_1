using Assets.Scripts.Conditions;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public CompositeCondition Condition = new();

        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private float _countdown;

        private Character _target;
        private float _currentTime;

        public void SetTarget(Character target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        private void FixedUpdate()
        {
            if (!Condition.IsTrue())
            {
                return;
            }
            
            if (!_target.IsHitPointsExists())
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(gameObject, startPosition, direction);
        }
    }
}