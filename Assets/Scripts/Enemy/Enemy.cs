using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private EnemyMoveAgent _enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent _enemyAttackAgent;


        public Action<Enemy> OnEnemyDie;
        public Action<Enemy, Vector2, Vector2> OnEnemyFire;

        private void OnEnable()
        {
            _hitPointsComponent.HpEmpty += Die;
            _enemyAttackAgent.OnFire += OnFire;
        }
        public void SetParent(Transform tr)
        {
            transform.SetParent(tr);
        }
        public void SetPosition(Transform tr)
        {
            transform.position = tr.position;
        }
        public void SetTargetDestination(Transform tr)
        {
            _enemyMoveAgent.SetDestination(tr.position);
        }
        public void SetTarget(GameObject target)
        {
            _enemyAttackAgent.SetTarget(target);
        }
        public void Die(GameObject enemy)
        {
            _hitPointsComponent.HpEmpty -= Die;
            _enemyAttackAgent.OnFire -= OnFire;
            OnEnemyDie?.Invoke(this);
        }
        public void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            /*_bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = false,
                physicsLayer = (int)PhysicsLayer.ENEMY,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });*/
            OnEnemyFire.Invoke(this, position, direction);
        }

    }

}
