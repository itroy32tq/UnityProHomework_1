using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
       fileName = "CharacterConfig",
       menuName = "Character/NewCharacterConfig"
   )]
    public sealed class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int HitPoints { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public Transform FirePoint { get; private set; }
        [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
    }
}
