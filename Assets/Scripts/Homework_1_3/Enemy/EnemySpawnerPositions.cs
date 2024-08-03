using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawnerPositions
    {
        private readonly Transform[] _spawnPositions;
        private readonly Transform[] _attackPositions;

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions);
        }

        public EnemySpawnerPositions(Transform[] spawnPositions, Transform[] attackPositions)
        {
            _spawnPositions = spawnPositions;
            _attackPositions = attackPositions;
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