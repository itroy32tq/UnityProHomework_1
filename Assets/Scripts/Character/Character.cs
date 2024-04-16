using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] WeaponComponent _weaponComponent;
        [SerializeField] HitPointsComponent _hitPointsComponent;
        [SerializeField] TeamComponent _teamComponent;
        [SerializeField] InputManager _inputManager;
        [SerializeField] BulletSystem _bulletSystem;

        //todo по умолчанию игрок стреляет только вверх
        [SerializeField] Vector2 _shootDirection = Vector2.up;

        private void Awake()
        {
            IGameListener.Register(this);
        }
        public Action<Character> OnCharacterDie;
        public void OnStartGame()
        {
            _hitPointsComponent.HpEmpty += Die;
            _inputManager.OnMove += _moveComponent.MoveByRigidbodyVelocity;
            _inputManager.OnShoot += Shoot;
            _weaponComponent.SetBulletSystem(_bulletSystem);
        }

        public void Move(Vector2 vector)
        {
            _moveComponent.MoveByRigidbodyVelocity(vector);
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
            _inputManager.OnMove -= _moveComponent.MoveByRigidbodyVelocity;
            _inputManager.OnShoot -= Shoot;
        }
    }
}
