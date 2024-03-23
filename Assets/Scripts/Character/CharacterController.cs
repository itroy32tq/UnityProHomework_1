using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {

        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        [SerializeField] WeaponComponent _weaponComponent;
        [SerializeField] HitPointsComponent _hitPointsComponent;

        public event Action OnCharacterDieEvent;


        private bool _fireRequired;
        public bool FireRequired { get => _fireRequired; set => _fireRequired = value; }

        private void OnEnable()
        {
            _hitPointsComponent.HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.HpEmpty -= OnCharacterDeath;
            
        }

        private void OnCharacterDeath(GameObject _) => OnCharacterDieEvent?.Invoke();

        private void FixedUpdate()
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) _bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = _weaponComponent.Position,
                velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}