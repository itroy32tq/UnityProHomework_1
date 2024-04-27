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
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private LevelBounds _levelBounds;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> _allBulletsList = new();

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            _bulletPool = new Pool<Bullet>(_initialCount, new Factory<Bullet>(_prefab, _container));
        }

        public void Create(Args args)
        {
            if (_bulletPool.TryGet(out Bullet bullet))
            {
                bullet.Construct(args);
                _allBulletsList.Add(bullet);
                bullet.OnDesrtoyHandler += OnBulletCollision;
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
                bullet.OnDesrtoyHandler -= OnBulletCollision;
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