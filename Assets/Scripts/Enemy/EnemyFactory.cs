using Assets.Scripts.Interface;
using Assets.Scripts.Inventary;
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyFactory : IFactory<Enemy>
    {
        private HitPointsComponent _hitPointsComponent;
        private EnemyMoveAgent _enemyMoveAgent;
        private EnemyAttackAgent _enemyAttackAgent;
        private WeaponComponent _weaponComponent;
        private MoveComponent _moveComponent;
        private Character _character;
        private GameObject _prefab;
        private EnemyConfig _config;

        public Action<IGameListener> OnCreateListener;

        public Enemy Create()
        {
            _prefab = UnityEngine.Object.Instantiate(_prefab);

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
        }
    }
}
