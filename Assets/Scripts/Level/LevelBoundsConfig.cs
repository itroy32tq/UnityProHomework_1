using UnityEngine;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu(
        fileName = "LevelBoundsConfig",
        menuName = "LevelBounds/New LevelBoundsConfigConfig"
    )]
    public sealed class LevelBoundsConfig : ScriptableObject
    {
        [field: SerializeField] public Transform LeftBorder { get; private set; }
        [field: SerializeField] public Transform RightBorder { get; private set; }
        [field: SerializeField] public Transform DownBorder { get; private set; }
        [field: SerializeField] public Transform TopBorder { get; private set; }
    }
}
