using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent
    {
        [field: SerializeField] public bool IsPlayer { get; private set; }

        public void Construct(bool isPlayer)
        { 
            IsPlayer = isPlayer;
        }
    }
}