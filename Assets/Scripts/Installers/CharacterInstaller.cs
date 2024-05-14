using Assets.Scripts.InfroStructure;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class CharacterInstaller : Installer
    {
        [SerializeField] private CharacterConfig _characterConfig;

        private readonly MoveComponent _moveComponent = new();
        private readonly WeaponComponent _weaponComponent = new();
        private readonly HitPointsComponent _hitPointsComponent = new();
        private readonly TeamComponent _teamComponent = new();

        [Listener] private readonly Character _character = new();
        [Listener] private readonly CharacterDethObserver _characterController = new();
        [Listener] private readonly InputManager _inputManager = new();


        public override void Install(DiContainer container)
        {
            container.AddService<CharacterConfig>(_characterConfig);
            container.AddService<Character>(_character);
            container.AddService<CharacterDethObserver>(_characterController);

            container.AddService<MoveComponent>(_moveComponent);
            container.AddService<WeaponComponent>(_weaponComponent);
            container.AddService<HitPointsComponent>(_hitPointsComponent);
            container.AddService<TeamComponent>(_teamComponent);
            container.AddService<InputManager>(_inputManager);
        }
    }
}
