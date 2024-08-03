using System;

namespace ShootEmUp
{
    public sealed class HitPointsComponent
    {

        public event Action<object> OnHitPointsEnding;

        public int TakeDamage(object sender, int damage, int hitPoints)
        {
            hitPoints -= damage;

            if (hitPoints <= 0)
            {
                OnHitPointsEnding?.Invoke(sender);
            }
            return hitPoints;
        }
    }
}