using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyFactory : IFactory<Enemy>
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly EnemyMoveAgent _enemyMoveAgent;
        private readonly EnemyAttackAgent _enemyAttackAgent;
        private readonly WeaponComponent _weaponComponent;
        private readonly MoveComponent _moveComponent;
        private readonly Character _character;
        private readonly EnemyConfig _config;
        private readonly Transform _container;
        private GameObject _prefab;

        public Action<IGameListener> OnCreateListener;

        public Enemy Create()
        {
            _prefab = UnityEngine.Object.Instantiate(_prefab, _container);

            Enemy enemy = new(_character, _hitPointsComponent, _enemyMoveAgent, _enemyAttackAgent,
                _weaponComponent, _moveComponent, _prefab, _config);

            OnCreateListener(enemy);
            return enemy;
        }

        public EnemyFactory(Character character, HitPointsComponent hitPointsComponent,
            EnemyMoveAgent enemyMoveAgent, EnemyAttackAgent enemyAttackAgent,
            WeaponComponent weaponComponent, MoveComponent moveComponent, EnemyConfig config)
        {
            _hitPointsComponent = hitPointsComponent;
            _enemyMoveAgent = enemyMoveAgent;
            _enemyAttackAgent = enemyAttackAgent;
            _weaponComponent = weaponComponent;
            _moveComponent = moveComponent;
            _character = character;
            _prefab = config.Prefab;
            _config = config;
            _container = config.Container;
        }
    }
}
