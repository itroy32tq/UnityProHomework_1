using Assets.Scripts.Conditions;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : IPrefable, IGameFixedUpdateListener
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly EnemyMoveAgent _enemyMoveAgent;
        private readonly EnemyAttackAgent _enemyAttackAgent;
        private readonly WeaponComponent _weaponComponent;
        private readonly MoveComponent _moveComponent;
        private readonly BulletConfig _bulletConfig;
        private readonly Character _character;
        private readonly GameObject _prefab;
        private readonly Transform _firePoint;
        private readonly int _hitPoints;
        private readonly float _speed;

        public WeaponComponent WeaponComponent => _weaponComponent;
        public GameObject Prefab =>_prefab;
        public bool IsPlayer { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        public Action<Enemy> OnEnemyDieingHandler;
        public Action<Enemy> OnEnemyFiringHandler;
        public readonly AndCondition AttackAgentCondition = new();


        public Enemy(Character character, HitPointsComponent hitPointsComponent, 
            EnemyMoveAgent enemyMoveAgent, EnemyAttackAgent enemyAttackAgent, 
            WeaponComponent weaponComponent, MoveComponent moveComponent, 
            GameObject prefab, EnemyConfig config)
        { 
            _hitPointsComponent = hitPointsComponent; 
            _enemyMoveAgent = enemyMoveAgent; 
            _enemyAttackAgent = enemyAttackAgent;
            _weaponComponent = weaponComponent; 
            _moveComponent = moveComponent;
            _bulletConfig = config.BulletConfig;
            _character = character;
            _prefab = prefab;

            Rigidbody = _prefab.GetComponent<Rigidbody2D>();
            _speed = config.Speed;
            _firePoint = config.FirePoint;
            _hitPoints = config.HitPoints;
            IsPlayer = config.IsPlayer;
        }
      
        public void Start()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _enemyAttackAgent.OnEnemyFireingHandler += OnFire;

            AttackAgentCondition.Append(_character.IsHitPointsExists);
            AttackAgentCondition.Append(IsReached);

            _enemyMoveAgent.OnMove += Move;
        }

        public void CollisionHandler(int damage)
        {
            _hitPointsComponent.TakeDamage(damage, _hitPoints);
        }

        public bool GetTeam()
        {
            return IsPlayer;
        }
        public void Move(Vector2 vector)
        {
            _moveComponent.Move(Rigidbody, vector, _speed);
        }

        private bool IsReached()
        { 
            return _enemyMoveAgent.IsReached;
        }

        public void SetParent(Transform tr)
        {
            _prefab.transform.SetParent(tr);
        }

        public void SetPosition(Transform tr)
        {
            _prefab.transform.position = tr.position;
        }

        public void SetTargetDestination(Transform tr)
        {
            _enemyMoveAgent.SetDestination(tr.position);
        }
        
        private void Die()
        {
            _hitPointsComponent.OnHitPointsEnding -= Die;
            _enemyAttackAgent.OnEnemyFireingHandler -= OnFire;
            _enemyMoveAgent.OnMove -= Move;
            OnEnemyDieingHandler?.Invoke(this);
            AttackAgentCondition.Clear();
        }

        private void OnFire()
        {
            _weaponComponent.Shoot(IsPlayer, _enemyMoveAgent.Direction, _bulletConfig, _firePoint);
            OnEnemyFiringHandler?.Invoke(this);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _enemyAttackAgent.Tick(fixedDeltaTime, AttackAgentCondition);
            _enemyMoveAgent.Tick(fixedDeltaTime, _prefab.transform.position);
        }
    }

}
