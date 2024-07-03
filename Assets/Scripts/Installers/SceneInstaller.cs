using Assets.Scripts.Bullets;
using Assets.Scripts.Factory;
using Assets.Scripts.GenericPool;
using Assets.Scripts.Inventary;
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
        
        [SerializeField] private BulletSystemConfig _bulletSystemConfig;
        
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private EnemyAgentsConfig _enemyAgentsConfig;
        [SerializeField] private Transform _worldContainer;

        private PrefablePool<Enemy> _enemyPool;
        private Pool<Bullet> _bulletPool;

        [SerializeField] private GameManager _gameManager;

        private readonly MoveComponent _moveComponent = new();
        private readonly WeaponComponent _weaponComponent = new();
        private readonly HitPointsComponent _hitPointsComponent = new();

        [Listener] private readonly EnemySpawner _enemySpawner = new();
        private readonly EnemySpawnerPositions _enemySpawnerPositions = new();
        private readonly EnemyMoveAgent _enemyMoveAgent = new();
        private readonly EnemyAttackAgent _enemyAttackAgent = new();
        

        private Character _character;
        [Listener] private readonly CharacterDethObserver _characterController = new();
        [Listener] private readonly InputManager _inputManager = new();

        

        [Listener] private readonly BulletSystem _bulletSystem = new();
        
        [Listener, SerializeField] private Bullet _bullet;

        public override void Install(DiContainer container)
        {
            CreateCharacter();
            CreateEnemys();
            CreateBulletPool();

            container.AddService<Bullet>(_bullet);
            container.AddService<BulletSystemConfig>(_bulletSystemConfig);
            container.AddService<BulletSystem>(_bulletSystem);
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
            container.AddService<Pool<Bullet>>(_bulletPool);
            container.AddService<EnemySpawnerConfig>(_enemySpawnerConfig);
            container.AddService<EnemySpawnerPositions>(_enemySpawnerPositions);
            container.AddService<EnemyAttackAgent>(_enemyAttackAgent);
            container.AddService<EnemyMoveAgent>(_enemyMoveAgent);
            container.AddService<EnemySpawner>(_enemySpawner);
        }

        private void CreateBulletPool()
        {
            Transform container = Instantiate(_bulletSystemConfig.Container);
            IFactory<Bullet> factory = new Factory<Bullet>(_bullet, container);
            _bulletPool = new Pool<Bullet>(_bulletSystemConfig.InitialCount, factory);

        }

        private void CreateCharacter()
        {
            CharacterFactory characterFactory  = new(_worldContainer,
                                                     _moveComponent,
                                                     _weaponComponent,
                                                     _hitPointsComponent,
                                                     _inputManager,
                                                     _characterConfig);

            characterFactory.OnCreateListener += _gameManager.AddListner;
            _character = characterFactory.Create();
        }

        private void CreateEnemys()
        {
            EnemyFactory enemyFactory = new(_character,
                                            _hitPointsComponent,
                                            _enemyMoveAgent,
                                            _enemyAttackAgent,
                                            _weaponComponent,
                                            _moveComponent,
                                            _enemyConfig,
                                            _worldContainer);

            enemyFactory.OnCreateListener += _gameManager.AddListner;

            int initialSize = _enemySpawnerConfig.InitialCount;

            _enemyPool = new(initialSize, enemyFactory);
        }

        private void OnDisable()
        {
            //enemyFactory.OnCreateListener -= _gameManager.AddListner;
        }
    }
}
