using Assets.Scripts.Conditions;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public CompositeCondition Condition = new();

        public delegate void FireHandler();

        public event FireHandler OnFire;
        
        [SerializeField] private float _countdown;

        private float _currentTime;


        public void Reset()
        {
            _currentTime = _countdown;
        }

        private void FixedUpdate()
        {
            if (!Condition.IsTrue())
            {
                return;
            }
            
           
            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            OnFire?.Invoke();
        }
    }
}