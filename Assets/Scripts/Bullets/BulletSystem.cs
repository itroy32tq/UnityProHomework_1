using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameStartListener, IGameFixedUpdateListener
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> allBulletsList = new();

        private void Awake()
        {
            IGameListener.Register(this);
        }
        private void Construct(Bullet bullet, Transform container, Transform worldTransform, LevelBounds levelBounds)
        {
            _bullet = bullet; _container = container; _worldTransform = worldTransform; _levelBounds = levelBounds;
        }
        public void OnStartGame()
        {
            _bulletPool = new Pool<Bullet>(_initialCount, new Factory<Bullet>(_bullet, _container));
        }

        public void Create(Args args)
        {
            if (_bulletPool.TryGet(out Bullet bullet))
            {
                bullet.SetArgsToBullet(args);
                allBulletsList.Add(bullet);
                bullet.OnDestoy += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (allBulletsList.Remove(bullet))
            {
                bullet.OnDestoy -= OnBulletCollision;
                _bulletPool.Release(bullet);
            }
        }

        public void OnFixedUpdate(float deltatime)
        {
            var notBoundsBullet = allBulletsList.Where(x => !_levelBounds.InBounds(x.transform.position)).ToList();

            if (!notBoundsBullet.Any())
            {
                return;
            }

            foreach (var bullet in notBoundsBullet)
            {
                RemoveBullet(bullet);
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