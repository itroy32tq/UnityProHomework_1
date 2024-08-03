using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "EnemySpawnerConfig",
       menuName = "EnemySpawner/NewEnemySpawnerConfig"
   )]

    public sealed class EnemySpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public Transform[] SpawnPositions { get; private set; }
        [field: SerializeField] public Transform[] AttackPositions { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public int InitialCount { get; private set; }
    }
}
