using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "EnemyConfig",
       menuName = "Enemy/NewEnemyConfig"
   )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int HitPoints { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
        [field: SerializeField] public Transform Container { get; private set; }
    }
}
