using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [field: SerializeField] public bool IsPlayer { get; private set; }
    }
}