using Assets.Scripts.Conditions;
using Assets.Scripts.Interface;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _countdown;
        private float _currentTime;

        public readonly AndCondition AttackAgentCondition = new();
        public event Action OnEnemyFireingHandler;
        
        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        private void Construct(float countdown)
        {
            _countdown = countdown;
        }
        private void Fire()
        {
            OnEnemyFireingHandler?.Invoke();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!AttackAgentCondition.IsTrue())
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