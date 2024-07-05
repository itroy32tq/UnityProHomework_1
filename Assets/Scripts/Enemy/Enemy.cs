using Assets.Scripts.Conditions;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : IPrefable, IGameFixedUpdateListener, IGameStartListener
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
        private int _hitPoints;
        private readonly float _speed;

        public bool IsReached { get; private set; }
        public Vector2 Destination { get; private set; } = Vector2.one;
        public int HitPoints => _hitPoints;
        public WeaponComponent WeaponComponent => _weaponComponent;
        public GameObject Prefab => _prefab;
        public bool IsPlayer { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        public Action<Enemy> OnEnemyDieingHandler;
        public Action<Enemy> OnEnemyFiringHandler;
        public readonly AndCondition AttackAgentCondition = new();


        public Enemy(Character character, HitPointsComponent hitPointsComponent, EnemyMoveAgent enemyMoveAgent,
                     EnemyAttackAgent enemyAttackAgent, WeaponComponent weaponComponent, MoveComponent moveComponent,
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
            _firePoint = _prefab.GetComponentInChildren<Transform>();
            _hitPoints = config.HitPoints;
            IsPlayer = config.IsPlayer;
        }
      
        public void OnStartGame()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _enemyAttackAgent.OnEnemyFireingHandler += OnFire;
            AttackAgentCondition.Append(_character.IsCharacterLive);
            AttackAgentCondition.Append(() => IsReached == true);
            _enemyMoveAgent.OnMove += Move;
        }

        public void CollisionHandler(int damage)
        {
            _hitPoints = _hitPointsComponent.TakeDamage(this, damage, _hitPoints);
        }

        public bool GetTeam()
        {
            return IsPlayer;
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.Move(Rigidbody, vector, _speed);
        }

        public void SetReachedState(bool state)
        {
            IsReached = state;
        }

        public void SetPosition(Transform tr)
        {
            _prefab.transform.position = tr.position;
        }

        public void SetTargetDestination(Transform tr)
        {
            Destination = tr.position;
        }
        
        private void Die(object sender)
        {
            if (sender == this)
            {
                _hitPointsComponent.OnHitPointsEnding -= Die;
                _enemyAttackAgent.OnEnemyFireingHandler -= OnFire;
                _enemyMoveAgent.OnMove -= Move;
                OnEnemyDieingHandler?.Invoke(this);
                AttackAgentCondition.Clear();
            }
        }

        private void OnFire(Vector2 direction)
        {
            _weaponComponent.Shoot(IsPlayer, direction, _bulletConfig, _firePoint);
            OnEnemyFiringHandler?.Invoke(this);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _enemyAttackAgent.Tick(fixedDeltaTime, AttackAgentCondition, _prefab.transform);
            _enemyMoveAgent.Tick(this, fixedDeltaTime, _prefab.transform.position);
        }
    }
}
