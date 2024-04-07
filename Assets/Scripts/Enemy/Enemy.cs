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
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private TeamComponent _teamComponent;

        public WeaponComponent WeaponComponent => _weaponComponent;
        public Character Character { get; set; }

        public Action<Enemy> OnEnemyDie;
        public Action<Enemy> OnEnemyFire;

        private void OnEnable()
        {
            _hitPointsComponent.HpEmpty += Die;
            _enemyAttackAgent.OnFire += OnFire;
        }
        private void Start()
        {
            _enemyAttackAgent.Condition?.Append(Character.IsHitPointsExists);
            _enemyAttackAgent.Condition?.Append(IsReached);
        }
        public bool IsReached()
        { 
            return _enemyMoveAgent.IsReached;
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
   
        public void Die(GameObject enemy)
        {
            _hitPointsComponent.HpEmpty -= Die;
            _enemyAttackAgent.OnFire -= OnFire;
            OnEnemyDie?.Invoke(this);
        }
        public void OnFire()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _enemyMoveAgent.Direction);
            OnEnemyFire?.Invoke(this);
        }

    }

}
