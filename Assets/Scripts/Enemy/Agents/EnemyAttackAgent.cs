using Assets.Scripts.Conditions;
using Assets.Scripts.InfroStructure;
using System;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        private float _countdown;
        private float _currentTime;

        
        public event Action OnEnemyFireingHandler;
        
        public void Reset()
        {
            _currentTime = _countdown;
        }

        [Inject]
        public void Construct(EnemyAgentsConfig config)
        {
            _countdown = config.Countdown;
        }

        private void Fire()
        {
            OnEnemyFireingHandler?.Invoke();
        }

        public void Tick(float fixedDeltaTime, AndCondition condition)
        {
            if (!condition.IsTrue())
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