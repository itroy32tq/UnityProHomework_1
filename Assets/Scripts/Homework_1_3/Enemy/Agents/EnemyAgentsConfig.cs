using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "EnemyAgentsConfig",
       menuName = "EnemyAgents/NewEnemyAgentsConfig"
   )]
    public sealed class EnemyAgentsConfig : ScriptableObject
    {
        [field: SerializeField] public float Countdown { get; private set; }
    }
}
