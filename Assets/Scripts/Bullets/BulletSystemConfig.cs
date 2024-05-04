using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
    [CreateAssetMenu(
        fileName = "BulletSystemConfig",
        menuName = "Bullets/New BulletSystemConfig"
    )]
    public sealed class BulletSystemConfig : ScriptableObject
    {
        [field: SerializeField] public Bullet Bullet { get; private set; }
        [field: SerializeField] public int InitialCount { get; private set; }
        [field: SerializeField] public Transform Container { get; private set; }
    }
}
