using Assets.Scripts.InfroStructure;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {
        private Transform _firePoint;
        private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;

        [Inject]
        public void Construct(CharacterConfig config, BulletConfig bulletConfig, BulletSystem bulletSystem)
        { 
            _firePoint = config.FirePoint; _bulletConfig = bulletConfig; _bulletSystem = bulletSystem;
        }

        public void Shoot(bool isPlayer, Vector2 direction)
        {
            _bulletSystem.Create(new BulletSystem.Args
            {
                IsPlayer = isPlayer,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _firePoint.position,
                Velocity = _firePoint.rotation * direction * _bulletConfig.Speed
            });
        }
    }
}