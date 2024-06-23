using UnityEngine;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu(
        fileName = "LevelBackgroundConfig",
        menuName = "LevelBackground/New LevelBackgroundConfig"
    )]
    public sealed class LevelBackgroundConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject LevelBackground { get; private set; }
        [field: SerializeField] public float StartPositionY { get; private set; }
        [field: SerializeField] public float EndPositionY { get; private set; }
        [field: SerializeField] public float MovingSpeedY { get; private set; }
    }
}
