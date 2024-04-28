using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int _hitPoints;

        public event Action<GameObject> OnHitPointsEnding;

        public bool IsHitPointsExists() 
        {
            return _hitPoints > 0;
        }
        private void Construct(int hitPoints)
        { 
            _hitPoints = hitPoints;
        }
        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHitPointsEnding?.Invoke(gameObject);
            }
        }
    }
}