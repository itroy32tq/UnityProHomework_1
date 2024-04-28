using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [field: SerializeField] public bool IsPlayer { get; private set; }
        private void Construct(bool isPlayer)
        { 
            IsPlayer = isPlayer;
        }
    }
}