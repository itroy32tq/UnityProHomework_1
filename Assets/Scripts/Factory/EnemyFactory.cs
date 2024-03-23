using Assets.Scripts.Inventary;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Character _character;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] Enemy _prefab;
        [SerializeField] private EnemyController _enemyController;

        public Enemy Create()
        {
            var enemy = Instantiate(_prefab);
            enemy.SetParent(_worldTransform);

            enemy.OnEnemyDie += _enemyController.OnEnemyDieHandler;
            enemy.OnEnemyFire += _enemyController.OnEnemyFireHandler;
            enemy.SetPosition(_enemyPositions.RandomSpawnPosition());
            enemy.SetTargetDestination(_enemyPositions.RandomAttackPosition());
            enemy.SetTarget(_character);
            return enemy;
        }
    }
}
