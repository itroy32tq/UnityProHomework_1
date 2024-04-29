using Assets.Scripts.InfroStructure;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private EnemyMoveAgent _enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent _enemyAttackAgent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private MoveComponent _moveComponent;

        public WeaponComponent WeaponComponent => _weaponComponent;
        public Character Character { get; set; }
        public Action<Enemy> OnEnemyDieingHandler;
        public Action<Enemy> OnEnemyFiringHandler;

        [Inject]
        public void Construct(HitPointsComponent hitPointsComponent, EnemyMoveAgent enemyMoveAgent, 
            EnemyAttackAgent enemyAttackAgent, WeaponComponent weaponComponent, TeamComponent teamComponent, MoveComponent moveComponent)
        { 
            _hitPointsComponent = hitPointsComponent; _enemyMoveAgent = enemyMoveAgent; _enemyAttackAgent = enemyAttackAgent;
            _weaponComponent = weaponComponent; _teamComponent = teamComponent; _moveComponent = moveComponent;
        }
      
        private void Start()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _enemyAttackAgent.OnEnemyFireingHandler += OnFire;
            _enemyAttackAgent.AttackAgentCondition.Append(Character.IsHitPointsExists);
            _enemyAttackAgent.AttackAgentCondition.Append(IsReached);
            _enemyMoveAgent.OnMove += _moveComponent.Move;
        }

        private bool IsReached()
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
        
        private void Die(GameObject enemy)
        {
            _hitPointsComponent.OnHitPointsEnding -= Die;
            _enemyAttackAgent.OnEnemyFireingHandler -= OnFire;
            _enemyMoveAgent.OnMove -= _moveComponent.Move;
            OnEnemyDieingHandler?.Invoke(this);
            _enemyAttackAgent.AttackAgentCondition.Clear();
        }

        private void OnFire()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _enemyMoveAgent.Direction);
            OnEnemyFiringHandler?.Invoke(this);
        }
    }

}
