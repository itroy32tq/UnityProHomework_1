﻿using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Vector2 _shootDirection = Vector2.up;

        public Action OnCharacterDieingHandler;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        [Inject]
        public void Construct(MoveComponent moveComponent, WeaponComponent weaponComponent, 
            HitPointsComponent hitPointsComponent, TeamComponent teamComponent, InputManager inputManager, BulletSystem bulletSystem)
        {
            _moveComponent = moveComponent; _weaponComponent = weaponComponent; _hitPointsComponent = hitPointsComponent;
            _teamComponent = teamComponent; _inputManager = inputManager; _bulletSystem = bulletSystem;
        }
        public void OnStartGame()
        {
            _hitPointsComponent.OnHitPointsEnding += Die;
            _inputManager.OnInputMovingHandler += _moveComponent.Move;
            _inputManager.OnInputShootingHandler += Shoot;
            _weaponComponent.SetBulletSystem(_bulletSystem);
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnHitPointsEnding -= Die;
            _inputManager.OnInputMovingHandler -= _moveComponent.Move;
            _inputManager.OnInputShootingHandler -= Shoot;
        }

        private void Die(GameObject character)
        {
            OnCharacterDieingHandler?.Invoke(this);
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
