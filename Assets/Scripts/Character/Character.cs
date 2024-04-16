using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{

    public class Character : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Vector2 _shootDirection = Vector2.up;

        public Action<Character> OnCharacterDie;

        private void Awake()
        {
            IGameListener.Register(this);
        }
        
        public void OnStartGame()
        {
            _hitPointsComponent.HpEmpty += Die;
            _inputManager.OnMove += _moveComponent.Move;
            _inputManager.OnShoot += Shoot;
            _weaponComponent.SetBulletSystem(_bulletSystem);
        }

        private void OnDisable()
        {
            _hitPointsComponent.HpEmpty -= Die;
            _inputManager.OnMove -= _moveComponent.Move;
            _inputManager.OnShoot -= Shoot;
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.Move(vector);
        }

        public void Die(GameObject character)
        {
            _hitPointsComponent.HpEmpty -= Die;
            OnCharacterDie?.Invoke(this);
        }

        public void Shoot()
        {
            _weaponComponent.Shoot(_teamComponent.IsPlayer, _shootDirection);
        }

        public bool IsHitPointsExists()
        { 
            return _hitPointsComponent.IsHitPointsExists();
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.HpEmpty -= Die;
            _inputManager.OnMove -= _moveComponent.Move;
            _inputManager.OnShoot -= Shoot;
        }
    }
}
