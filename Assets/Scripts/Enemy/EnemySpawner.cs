using Assets.Scripts.GenericPool;
using Assets.Scripts.Inventary;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        private Pool<Enemy> _enemyPool;
        private readonly HashSet<Enemy> m_activeEnemies = new();
        private float _timer;

        [SerializeField] private IFactory<Enemy> _enemyFactory; 
        [SerializeField] private Enemy _prefab;
        [SerializeField] private float _spawnDelay = 1f;

        [Header("Pool")]
        [SerializeField] private int _initialCount = 7;
        
        private void Start()
        {
    
            _enemyPool = new Pool<Enemy>(_initialCount, _enemyFactory);
        }

        public void Update()
        {
  
            _timer += Time.deltaTime;

            if (_timer < _spawnDelay) return;

            Enemy enemy = _enemyPool.TryGet();
            if (enemy != null)
            {
                if (m_activeEnemies.Add(enemy))
                {
                    enemy.OnEnemyDie += RemoveEnemy;
                }
            }
            _timer = 0f;
        }

        private void RemoveEnemy(Enemy enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                _enemyPool.Release(enemy);
                enemy.OnEnemyDie -= RemoveEnemy;
            }

        }
    }
}