using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "EnemyConfig",
       menuName = "Enemy/NewEnemyConfig"
   )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField] public int HitPoints { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public Transform FirePoint { get; private set; }
        [field: SerializeField] public Transform[] SpawnPositions { get; private set; }
        [field: SerializeField] public Transform[] AttackPositions { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public int InitialCount { get; private set; }

    }
}
