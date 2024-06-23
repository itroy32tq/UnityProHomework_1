using Assets.Scripts.Conditions;
using Assets.Scripts.InfroStructure;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        private float _countdown;
        private float _currentTime;
        private Transform _target;


        public event Action<Vector2> OnEnemyFireingHandler;
        
        public void Reset()
        {
            _currentTime = _countdown;
        }

        [Inject]
        public void Construct(EnemyAgentsConfig config, CharacterConfig characterConfig)
        {
            _countdown = config.Countdown;
            _target = characterConfig.Prefab.transform;
        }

        private void Fire(Vector2 direction)
        {
            OnEnemyFireingHandler?.Invoke(direction);
        }

        public void Tick(float fixedDeltaTime, AndCondition condition, Transform transform)
        {
            if (!condition.IsTrue())
            {
                return;
            }

            _currentTime -= fixedDeltaTime;

            if (_currentTime <= 0)
            {
                var direction = _target.position - transform.position;

                Fire(direction.normalized);
                _currentTime += _countdown;
            }
        }
    }
}