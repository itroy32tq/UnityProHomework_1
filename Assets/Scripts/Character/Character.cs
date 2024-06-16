using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : IGameStartListener, IGameFinishListener, ITeamComponent
    {
        private MoveComponent _moveComponent;
        private WeaponComponent _weaponComponent;
        private HitPointsComponent _hitPointsComponent;
        private InputManager _inputManager;
        private Bullet _bullet;
        private GameObject _prefab;
        private CharacterConfig _config;
        private float _speed;
        private int _hitPoints;
        private BulletConfig _bulletConfig;
        private Transform _firePoint;

        private Vector2 _shootDirection = Vector2.up;

        public Action OnCharacterDieingHandler;
        public Rigidbody2D Rigidbody { get; private set; }
        public bool IsPlayer { get; private set; }

        [Inject]
        public void Construct(MoveComponent moveComponent, WeaponComponent weaponComponent,
            HitPointsComponent hitPointsComponent, InputManager inputManager, Bullet bullet, CharacterConfig config)
        {
            _moveComponent = moveComponent; 
            _weaponComponent = weaponComponent; 
            _hitPointsComponent = hitPointsComponent;
            _inputManager = inputManager; 

            _bullet = bullet;
            _prefab = config.Prefab;

            Rigidbody = _prefab.GetComponent<Rigidbody2D>();
            _speed = config.Speed;
            _config = config;
            _hitPoints = config.HitPoints;
            IsPlayer = config.IsPlayer;
            _bulletConfig = config.BulletConfig;
            _firePoint = config.FirePoint;
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _inputManager.OnInputMovingHandler += Move;
            _inputManager.OnInputShootingHandler += Shoot;
        }

        public bool GetTeam()
        {
            return IsPlayer;
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.Move(Rigidbody, vector, _speed);
        }

        private void Die()
        {
            OnCharacterDieingHandler?.Invoke();
        }

        public void OnBulletCollision(GameObject collisionObject, bool isPlayer, int damage)
        {
            if (!collisionObject == _prefab)
            {
                return;
            }

            if (isPlayer == GetTeam())
            {
                return;
            }

            _hitPoints = _hitPointsComponent.TakeDamage(damage, _hitPoints);
        }

        private void Shoot()
        {
            _weaponComponent.Shoot(IsPlayer, _shootDirection, _bulletConfig, _firePoint);
        }

        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.OnHitPointsEnding -= Die;
            _inputManager.OnInputMovingHandler -= Move;
            _inputManager.OnInputShootingHandler -= Shoot;
        }
    }
}
