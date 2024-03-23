using ShootEmUp;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletSystem _bulletSystem;

        [SerializeField] private EnemyPositions _enemyPositions;

        [SerializeField] HitPointsComponent _hitPointsComponent;
        [SerializeField] EnemyMoveAgent _enemyMoveAgent;
        [SerializeField] EnemyAttackAgent _enemyAttackAgent;

        public event Action<Enemy> OnEnemyDieEvent;

        public HitPointsComponent EnemyHitComponent => GetComponent<HitPointsComponent>();

        public void Construct()
        {
            //пока не понял зачем это
            transform.SetParent(_worldTransform);

            _hitPointsComponent.HpEmpty += OnDestroyed;
            _enemyAttackAgent.OnFire += OnFire;

            transform.position = _enemyPositions.RandomSpawnPosition().position;
            _enemyMoveAgent.SetDestination(_enemyPositions.RandomAttackPosition().position);
            _enemyAttackAgent.SetTarget(_character);
        }

        public void OnDestroyed(GameObject enemy)
        {
            _hitPointsComponent.HpEmpty -= OnDestroyed;
            _enemyAttackAgent.OnFire -= OnFire;
            OnEnemyDieEvent?.Invoke(this);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = false,
                physicsLayer = (int)PhysicsLayer.ENEMY,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }
    }
}
