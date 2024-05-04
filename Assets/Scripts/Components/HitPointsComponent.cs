using System;

namespace ShootEmUp
{
    public sealed class HitPointsComponent
    {
        private int _hitPoints;

        public event Action OnHitPointsEnding;

        public bool IsHitPointsExists() 
        {
            return _hitPoints > 0;
        }

        
        public void Construct(int hitPoints)
        { 
            _hitPoints = hitPoints;
        }
        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHitPointsEnding?.Invoke();
            }
        }
    }
}