using Assets.Scripts.InfroStructure;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;

        public void SetBulletSystem(BulletSystem bulletSystem)
        { 
            _bulletSystem = bulletSystem;
        }

        [Inject]
        public void Construct(Transform firePoint, BulletConfig bulletConfig, BulletSystem bulletSystem)
        { 
            _firePoint = firePoint; _bulletConfig = bulletConfig; _bulletSystem = bulletSystem;
        }

        public void Shoot(bool isPlayer, Vector2 direction)
        {
            _bulletSystem.Create(new BulletSystem.Args
            {
                isPlayer = isPlayer,
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = _firePoint.position,
                velocity = _firePoint.rotation * direction * _bulletConfig.Speed
            });
        }
    }
}