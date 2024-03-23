using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public interface IBulletInfo
    {
        public PhysicsLayer PhysicsLayer { get; }
        public Color Color { get; }
        public int Damage { get; }
        public float Speed { get; }
    }

    
}
