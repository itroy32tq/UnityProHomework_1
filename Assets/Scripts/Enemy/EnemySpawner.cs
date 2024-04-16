using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStartListener
    {
        private Pool<Enemy> _enemyPool;
        private readonly List<Enemy> m_activeEnemies = new();
        private float _timer;

        [SerializeField] private EnemyFactory _enemyFactory; 
        [SerializeField] private Enemy _prefab;
        [SerializeField] private float _spawnDelay = 1f;

        [Header("Pool")]
        [SerializeField] private int _initialCount = 7;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            _enemyPool = new Pool<Enemy>(_initialCount, _enemyFactory);
        }

        public void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _spawnDelay || _enemyPool == null) return;
          
            if (!_enemyPool.TryGet(out Enemy enemy))
            {
                return;
            }

            m_activeEnemies.Add(enemy);
            enemy.OnEnemyDie += RemoveEnemy;
         
            _timer = 0f;
        }

        private void RemoveEnemy(Enemy enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                _enemyPool?.Release(enemy);
                enemy.OnEnemyDie -= RemoveEnemy;
            }
        }
    }
}