using Assets.Scripts.InfroStructure;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawnerPositions
    {
        private Transform[] _spawnPositions;
        private Transform[] _attackPositions;

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions);
        }

        [Inject]
        public void Construct(EnemySpawnerConfig config)
        { 
            _attackPositions = config.AttackPositions; 
            _spawnPositions = config.SpawnPositions;
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(_attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}