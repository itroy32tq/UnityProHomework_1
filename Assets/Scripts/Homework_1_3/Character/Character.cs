using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : IGameStartListener, IGameFinishListener
    {
        private readonly MoveComponent _moveComponent;
        private readonly WeaponComponent _weaponComponent;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly InputManager _inputManager;
        private readonly GameObject _prefab;
        private readonly float _speed;
        private readonly BulletConfig _bulletConfig;
        private readonly Transform _firePoint;
        private readonly Rigidbody2D _rigidbody;

        private int _hitPoints;

        private Vector2 _shootDirection = Vector2.up;
        public Action OnCharacterDieingHandler;
        public bool IsPlayer { get; private set; }

        public Character(MoveComponent moveComponent, WeaponComponent weaponComponent, HitPointsComponent hitPointsComponent, 
            InputManager inputManager, CharacterConfig config, GameObject prefab, Rigidbody2D rigidbody, Transform firePoint)
        {
            _moveComponent = moveComponent; 
            _weaponComponent = weaponComponent; 
            _hitPointsComponent = hitPointsComponent;
            _inputManager = inputManager; 

            _prefab = prefab;
            _rigidbody = rigidbody;
            _speed = config.Speed;
            _hitPoints = config.HitPoints;
            IsPlayer = config.IsPlayer;
            _bulletConfig = config.BulletConfig;
            _firePoint = firePoint;
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _inputManager.OnInputMovingHandler += Move;
            _inputManager.OnInputShootingHandler += Shoot;
        }

        public Transform GetTransform()
        {
            return _prefab.transform;
        }

        public bool GetTeam()
        {
            return IsPlayer;
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.Move(_rigidbody, vector, _speed);
        }

        private void Die(object sender)
        {
            if (sender is Character)
            {
                OnCharacterDieingHandler?.Invoke();
            }
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

            _hitPoints = _hitPointsComponent.TakeDamage(this, damage, _hitPoints);
        }

        private void Shoot()
        {
            _weaponComponent.Shoot(IsPlayer, _shootDirection, _bulletConfig, _firePoint);
        }

        public bool IsCharacterLive()
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
