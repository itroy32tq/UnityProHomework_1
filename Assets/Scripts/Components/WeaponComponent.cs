using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;

        public Vector2 Position => _firePoint.position;
        public Quaternion Rotation => _firePoint.rotation;
        public void SetBulletSystem(BulletSystem bulletSystem)
        { 
            _bulletSystem = bulletSystem;
        }
        public void Shoot(bool isPlayer, Vector2 direction)
        {
            _bulletSystem.Create(new BulletSystem.Args
            {
                isPlayer = isPlayer,
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = Position,
                velocity = Rotation * direction * _bulletConfig.Speed
            });
        }
    }
}