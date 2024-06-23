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
            _spawnPositions = new Transform[config.SpawnPositions.Length];

            for (int i = 0; i < config.SpawnPositions.Length; i++)
            {
                var spawnPos = Object.Instantiate(config.SpawnPositions[i]);
                _spawnPositions[i] = spawnPos;
            }

            _attackPositions = new Transform[config.AttackPositions.Length];

            for (int i = 0; i < config.AttackPositions.Length; i++)
            {
                var attackPos = Object.Instantiate(config.AttackPositions[i]);
                _attackPositions[i] = attackPos;
            }

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