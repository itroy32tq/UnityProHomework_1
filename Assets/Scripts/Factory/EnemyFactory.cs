using Assets.Scripts.InfroStructure;
using Assets.Scripts.Inventary;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public sealed class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] private Transform _container;
 
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] Enemy _prefab;

        public Enemy Create()
        {
            var enemy = Instantiate(_prefab);
            enemy.SetParent(_container);
            return enemy;
        }

        [Inject]
        public void Construct(Transform container, EnemyPositions enemyPositions)
        { 
            _container = container; _enemyPositions = enemyPositions; 
        }

        public void SetRandomAttackPosition(Enemy enemy)
        {
            enemy.SetTargetDestination(_enemyPositions.RandomAttackPosition());
        }

        public void SetRandomPosition(Enemy enemy)
        {
            enemy.SetPosition(_enemyPositions.RandomSpawnPosition());
        }
    }
}
