using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.InfroStructure;
using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : IGameStartListener, IGameUpdateListener
    {
        private Pool<Enemy> _enemyPool;
        private readonly List<Enemy> _activeEnemies = new();
        private float _timer;

        private EnemyPositions _enemyPositions;
        private Enemy _prefab;

        private Transform _container;

        private IFactory<Enemy> _enemyFactory; 

        private float _spawnDelay = 1f;
        private int _initialCount = 7;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        [Inject]
        public void Construct(EnemyConfig enemyConfig)
        {
            _prefab = enemyConfig.Prefab;
            _spawnDelay = enemyConfig.SpawnDelay; 
            _initialCount = enemyConfig.InitialCount;
        }

        public void OnStartGame()
        {
            _enemyFactory = new Factory<Enemy>(_prefab, _container);
            _enemyPool = new Pool<Enemy>(_initialCount, _enemyFactory);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                _enemyPool?.Release(enemy);
                enemy.OnEnemyDieingHandler -= RemoveEnemy;
            }
        }

        public void SetRandomAttackPosition(Enemy enemy)
        {
            enemy.SetTargetDestination(_enemyPositions.RandomAttackPosition());
        }

        public void SetRandomPosition(Enemy enemy)
        {
            enemy.SetPosition(_enemyPositions.RandomSpawnPosition());
        }

        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer < _spawnDelay || _enemyPool == null) return;

            if (!_enemyPool.TryGet(out Enemy enemy))
            {
                return;
            }

            SetRandomPosition(enemy);
            SetRandomAttackPosition(enemy);
            _activeEnemies.Add(enemy);
            enemy.OnEnemyDieingHandler += RemoveEnemy;

            _timer = 0f;
        }
    }
}