using Assets.Scripts;
using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Inventary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private Pool<Bullet> _bulletPool;
        private IFactory<Bullet> _bulletFactory;
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            _bulletFactory = new Factory<Bullet>(_prefab, _container);
            _bulletPool = new Pool<Bullet>(_initialCount, _bulletFactory);

        }
        
        private void FixedUpdate()
        {
            m_cache.Clear();
            m_cache.AddRange(m_activeBullets);

            var inBoundsBullet = m_cache.Where(x => _levelBounds.InBounds(x.transform.position));

            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                Bullet bullet = m_cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var bullet = _bulletPool.TryGet();
            bullet.Construct(args);

            if (m_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            bullet.DealDamage(collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletPool.Release(bullet);
            }
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}