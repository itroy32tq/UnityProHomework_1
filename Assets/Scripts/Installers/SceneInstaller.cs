using Assets.Scripts.Bullets;
using Assets.Scripts.Level;
using Assets.Scripts.Pools;
using ShootEmUp;
using System;
using UnityEngine;

namespace Assets.Scripts.InfroStructure
{
    public sealed class SceneInstaller : Installer
    {
        
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private LevelBoundsConfig _levelBoundsConfig;
        [SerializeField] private BulletSystemConfig _bulletSystemConfig;
        [SerializeField] private LevelBackgroundConfig _levelBackgroundConfig;
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private EnemyAgentsConfig _enemyAgentsConfig;

        private PrefablePool<Enemy> _enemyPool;

        [SerializeField] private GameManager _gameManager;

        private readonly MoveComponent _moveComponent = new();
        private readonly WeaponComponent _weaponComponent = new();
        private readonly HitPointsComponent _hitPointsComponent = new();

        [Listener] private readonly EnemySpawner _enemySpawner = new();
        private readonly EnemySpawnerPositions _enemySpawnerPositions = new();
        private readonly EnemyMoveAgent _enemyMoveAgent = new();
        private readonly EnemyAttackAgent _enemyAttackAgent = new();
        

        [Listener] private readonly Character _character = new();
        [Listener] private readonly CharacterDethObserver _characterController = new();
        [Listener] private readonly InputManager _inputManager = new();

        private readonly LevelBounds _levelBounds = new();

        [Listener] private readonly BulletSystem _bulletSystem = new();
        [Listener] private readonly LevelBackground _levelBackground = new();
        [Listener, SerializeField] private Bullet _bullet;

        public override void Install(DiContainer container)
        {
            CreateEnemys();
            CreateBullets();

            container.AddService<Bullet>(_bullet);
            container.AddService<BulletSystemConfig>(_bulletSystemConfig);
            container.AddService<LevelBackgroundConfig>(_levelBackgroundConfig);
            container.AddService<LevelBoundsConfig>(_levelBoundsConfig);

            container.AddService<LevelBounds>(_levelBounds);
            container.AddService<BulletSystem>(_bulletSystem);
            container.AddService<LevelBackground>(_levelBackground);
            container.AddService<GameManager>(_gameManager);

            container.AddService<CharacterConfig>(_characterConfig);
            container.AddService<Character>(_character);
            container.AddService<CharacterDethObserver>(_characterController);

            container.AddService<MoveComponent>(_moveComponent);
            container.AddService<WeaponComponent>(_weaponComponent);
            container.AddService<HitPointsComponent>(_hitPointsComponent);
            container.AddService<InputManager>(_inputManager);

            container.AddService<EnemyConfig>(_enemyConfig);
            container.AddService<EnemyAgentsConfig>(_enemyAgentsConfig);
            container.AddService<PrefablePool<Enemy>>(_enemyPool);
            container.AddService<EnemySpawnerConfig>(_enemySpawnerConfig);
            container.AddService<EnemySpawnerPositions>(_enemySpawnerPositions);
            container.AddService<EnemyAttackAgent>(_enemyAttackAgent);
            container.AddService<EnemyMoveAgent>(_enemyMoveAgent);
            container.AddService<EnemySpawner>(_enemySpawner);
        }

        private void CreateBullets()
        {
            _bulletSystem.OnCreateListener += _gameManager.AddListner;
        }

        private void CreateEnemys()
        {
            EnemyFactory enemyFactory = new(_character,
                                            _hitPointsComponent,
                                            _enemyMoveAgent,
                                            _enemyAttackAgent,
                                            _weaponComponent,
                                            _moveComponent,
                                            _enemyConfig);

            enemyFactory.OnCreateListener += _gameManager.AddListner;

            int initialSize = _enemySpawnerConfig.InitialCount;

            _enemyPool = new(initialSize, enemyFactory);
        }
    }
}
