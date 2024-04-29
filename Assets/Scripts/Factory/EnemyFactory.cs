using Assets.Scripts.InfroStructure;
using Assets.Scripts.Inventary;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public sealed class EnemyFactory : MonoBehaviour, IFactory<Enemy>
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Character _character;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] Enemy _prefab;
        [SerializeField] private BulletSystem _enemyBulletSystem;

        public Enemy Create()
        {
            Enemy enemy = Instantiate(_prefab);
            enemy.WeaponComponent.SetBulletSystem(_enemyBulletSystem);
            enemy.SetParent(_container);
            enemy.Character = _character;
            return enemy;
        }

        [Inject]
        public void Construct(Transform container, Character character, EnemyPositions enemyPositions, BulletSystem bulletSystem)
        { 
            _container = container; _character = character; _enemyPositions = enemyPositions; _enemyBulletSystem = bulletSystem;
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
