using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.InfroStructure;
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
        [SerializeField] private LevelBounds _levelBounds;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> _allBulletsList = new();

        private void Awake()
        {
            IGameListener.Register(this);
        }

        [Inject]
        public void Construct(Bullet bullet, Transform container, LevelBounds levelBounds)
        {
            _bullet = bullet; _container = container; _levelBounds = levelBounds;
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
                _allBulletsList.Add(bullet);
                bullet.OnBulletDestroyHandler += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_allBulletsList.Remove(bullet))
            {
                bullet.OnBulletDestroyHandler -= OnBulletCollision;
                _bulletPool.Release(bullet);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            List<Bullet> notBoundsBullet = new();
            for (int i = 0; i < _allBulletsList.Count; i++)
            {
                if (!_levelBounds.InBounds(_allBulletsList[i].transform.position))
                {
                    notBoundsBullet.Add(_allBulletsList[i]);
                }
            }

            if (notBoundsBullet.Count() == 0)
            {
                return;
            }

            for (int i = 0; i < notBoundsBullet.Count; i++)
            {
                RemoveBullet(notBoundsBullet[i]);
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