using Assets.Scripts.InfroStructure;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class EnemyInstaller : Installer
    {
        [SerializeField] EnemyConfig _enemyConfig;

        private readonly MoveComponent _moveComponent = new();
        private readonly WeaponComponent _weaponComponent = new();
        private readonly HitPointsComponent _hitPointsComponent = new();
        private readonly TeamComponent _teamComponent = new();

        [Listener] private readonly EnemyAttackAgent _enemyAttackAgent = new();
        [Listener] private readonly EnemyMoveAgent _enemyMoveAgent = new();
        [Listener] private readonly EnemySpawner _enemySpawner = new();


        public override void Install(DiContainer container)
        {
            container.AddService<EnemyConfig>(_enemyConfig);
            container.AddService<MoveComponent>(_moveComponent);
            container.AddService<WeaponComponent>(_weaponComponent);
            container.AddService<HitPointsComponent>(_hitPointsComponent);
            container.AddService<TeamComponent>(_teamComponent);
            container.AddService<EnemyAttackAgent>(_enemyAttackAgent);
            container.AddService<EnemyMoveAgent>(_enemyMoveAgent);
            container.AddService<EnemySpawner>(_enemySpawner);
        }
    }
}
