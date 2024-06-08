using Assets.Scripts.Bullets;
using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : IGameStartListener, IGameFixedUpdateListener
    {
        private int _initialCount;
        private Bullet _bullet;
        private LevelBounds _levelBounds;
        private Transform _container;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> _allBulletsList = new();
        public Action<IGameListener> OnCreateListener;

        [Inject]
        public void Construct(Bullet bullet, BulletSystemConfig config, LevelBounds levelBounds)
        {
            _bullet = bullet;
            _initialCount = config.InitialCount;
            _container = config.Container;
            _levelBounds = levelBounds;
        }

        public void OnStartGame()
        {
            IFactory<Bullet> factory = new Factory<Bullet>(_bullet, _container);
            _bulletPool = new Pool<Bullet>(_initialCount, factory);
        }

        public void Create(Args args)
        {
            if (_bulletPool.TryGet(out Bullet bullet))
            {
                bullet.SetArgsToBullet(args);
                _allBulletsList.Add(bullet);
                bullet.OnBulletDestroyHandler += OnBulletCollision;
                OnCreateListener.Invoke(bullet);
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
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}