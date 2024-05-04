using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : IGameStartListener, IGameFinishListener
    {
        private MoveComponent _moveComponent;
        private WeaponComponent _weaponComponent;
        private HitPointsComponent _hitPointsComponent;
        private TeamComponent _teamComponent;
        private InputManager _inputManager;
        private BulletSystem _bulletSystem;

        private Vector2 _shootDirection = Vector2.up;

        public Action OnCharacterDieingHandler;

        
        [Inject]
        public void Construct(CharacterConfig config, MoveComponent moveComponent, WeaponComponent weaponComponent,
            HitPointsComponent hitPointsComponent, TeamComponent teamComponent, InputManager inputManager, BulletSystem bulletSystem)
        {
            _moveComponent = moveComponent; _weaponComponent = weaponComponent; _hitPointsComponent = hitPointsComponent;
            _teamComponent = teamComponent; _inputManager = inputManager; _bulletSystem = bulletSystem;

            _moveComponent.Construct(config.Prefab.GetComponent<Rigidbody2D>(), config.Speed);
            _hitPointsComponent.Construct(config.HitPoints);
            _teamComponent.Construct(config.IsPlayer);
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _inputManager.OnInputMovingHandler += _moveComponent.Move;
            _inputManager.OnInputShootingHandler += Shoot;
        }

        private void Die()
        {
            OnCharacterDieingHandler?.Invoke();
        }

        private void Shoot()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _shootDirection);
        }

        public bool IsHitPointsExists()
        { 
            return _hitPointsComponent.IsHitPointsExists();
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.OnHitPointsEnding -= Die;
            _inputManager.OnInputMovingHandler -= _moveComponent.Move;
            _inputManager.OnInputShootingHandler -= Shoot;
        }
    }
}
