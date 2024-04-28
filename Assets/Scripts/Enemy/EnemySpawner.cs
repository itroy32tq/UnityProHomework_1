using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStartListener, IGameUpdateListener
    {
        private Pool<Enemy> _enemyPool;
        private readonly List<Enemy> _activeEnemies = new();
        private float _timer;

        [SerializeField] private EnemyFactory _enemyFactory; 
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _spawnDelay = 1f;
        [Header("Pool")]
        [SerializeField] private int _initialCount = 7;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        private void Construct(EnemyFactory enemyFactory, Enemy enemy, float spawnDelay, int initialCount)
        { 
            _enemyFactory = enemyFactory; _enemy = enemy; _spawnDelay = spawnDelay; _initialCount = initialCount;
        }

        public void OnStartGame()
        {
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

        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer < _spawnDelay || _enemyPool == null) return;

            if (!_enemyPool.TryGet(out Enemy enemy))
            {
                return;
            }

            _enemyFactory.SetRandomPosition(enemy);
            _enemyFactory.SetRandomAttackPosition(enemy);
            _activeEnemies.Add(enemy);
            enemy.OnEnemyDieingHandler += RemoveEnemy;

            _timer = 0f;
        }
    }
}