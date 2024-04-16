using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> m_cache = new();

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            _bulletPool = new Pool<Bullet>(_initialCount, new Factory<Bullet>(_prefab, _container));
        }

        private void FixedUpdate()
        {
            var notBoundsBullet = m_cache.Where(x => !_levelBounds.InBounds(x.transform.position));
            
            if (!notBoundsBullet.Any())
            {
                return;
            }

            foreach (var bullet in notBoundsBullet)
            {
                RemoveBullet(bullet);
            }
        }

        public void Create(Args args)
        {
            if (_bulletPool.TryGet(out Bullet bullet))
            {
                bullet.Construct(args);
                m_cache.Add(bullet);
                bullet.OnDestoy += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (m_cache.Remove(bullet))
            {
                bullet.OnDestoy -= OnBulletCollision;
                _bulletPool.Release(bullet);
                bullet.OnDestoy -= OnBulletCollision;
                _bulletPool?.Release(bullet);
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