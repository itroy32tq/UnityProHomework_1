using Assets.Scripts.InfroStructure;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {

        private BulletSystem _bulletSystem;

        [Inject]
        public void Construct(BulletSystem bulletSystem)
        { 
            _bulletSystem = bulletSystem;
        }

        public void Shoot(bool isPlayer, Vector2 direction, BulletConfig bulletConfig, Transform firePoint)
        {
            _bulletSystem.Create(new Args
            {
                IsPlayer = isPlayer,
                PhysicsLayer = (int)bulletConfig.PhysicsLayer,
                Color = bulletConfig.Color,
                Damage = bulletConfig.Damage,
                Position = firePoint.position,
                Velocity = firePoint.rotation * direction * bulletConfig.Speed
            });
        }
    }
}