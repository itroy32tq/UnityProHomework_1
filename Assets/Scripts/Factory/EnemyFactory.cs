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
        [SerializeField] private BulletSystem _enemyBulletSystem;

        public Enemy Create()
        {
            Enemy enemy = Instantiate(_prefab);
            enemy.WeaponComponent.SetBulletSystem(_enemyBulletSystem);
            enemy.SetParent(_worldTransform);
            enemy.Character = _character;
            enemy.OnEnemyDie += _enemyController.OnEnemyDieHandler;
            enemy.OnEnemyFire += _enemyController.OnEnemyFireHandler;
            enemy.SetPosition(_enemyPositions.RandomSpawnPosition());
            enemy.SetTargetDestination(_enemyPositions.RandomAttackPosition());
            return enemy;
        }
    }
}
