using Assets.Scripts.GenericPool;
using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : IGameFixedUpdateListener, IGamePauseListener, IGameResumeListener
    {
        private LevelBounds _levelBounds;
        private Character _character;
        private EnemySpawner _spawner;

        private Pool<Bullet> _bulletPool;
        private readonly List<Bullet> _allBulletsList = new();

        [Inject]
        public void Construct(Pool<Bullet> bulletPool, LevelBounds levelBounds, Character character, EnemySpawner spawner)
        {
            _levelBounds = levelBounds;
            _character = character;
            _spawner = spawner;
            _bulletPool = bulletPool;
        }

        public void Create(BulletsArgs args)
        {
            if (_bulletPool.TryGet(out Bullet bullet))
            {
                bullet.SetArgsToBullet(args);
                _allBulletsList.Add(bullet);
                bullet.OnBulletDestroyHandler += OnBulletCollision;
                bullet.OnBulletCollisionHandler += _spawner.OnBulletCollision;
                bullet.OnBulletCollisionHandler += _character.OnBulletCollision;
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

        public void OnPauseGame()
        {
            for (int i = 0; i < _allBulletsList.Count; i++)
            {
                _allBulletsList[i].PauseBullet();
            }
        }
       
        public void OnResumeGame()
        {
            for (int i = 0; i < _allBulletsList.Count; i++)
            {
                _allBulletsList[i].ResumeBullet();
            }
        }
    }
}