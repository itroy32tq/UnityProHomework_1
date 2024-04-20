using Assets.Scripts.Conditions;
using Assets.Scripts.Interface;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        public CompositeCondition Condition = new();

        public delegate void FireHandler();

        public event FireHandler OnFire;
        
        [SerializeField] private float _countdown;

        private float _currentTime;
        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        private void Fire()
        {
            OnFire?.Invoke();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!Condition.IsTrue())
            {
                return;
            }

            _currentTime -= fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }
    }
}