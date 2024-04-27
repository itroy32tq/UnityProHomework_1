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

        public Action<Character> OnCharacterDieingHandler;

        private void Awake()
        {
            IGameListener.Register(this);
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
