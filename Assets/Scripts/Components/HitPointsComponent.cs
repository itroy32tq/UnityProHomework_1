using System;

namespace ShootEmUp
{
    public sealed class HitPointsComponent
    {

        public event Action OnHitPointsEnding;

        public int TakeDamage(int damage, int hitPoints)
        {
            hitPoints -= damage;

            if (hitPoints <= 0)
            {
                OnHitPointsEnding?.Invoke();
            }
            return hitPoints;
        }
    }
}