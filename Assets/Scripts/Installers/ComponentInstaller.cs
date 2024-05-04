using Assets.Scripts.InfroStructure;
using ShootEmUp;

namespace Assets.Scripts.Installers
{
    public sealed class ComponentInstaller : Installer
    {
        private readonly MoveComponent _moveComponent = new();
        private readonly WeaponComponent _weaponComponent = new();
        private readonly HitPointsComponent _hitPointsComponent = new();
        private readonly TeamComponent _teamComponent = new();

        [Listener] private readonly InputManager _inputManager = new();

        public override void Install(DiContainer container)
        {
            container.AddService<MoveComponent>(_moveComponent);
            container.AddService<WeaponComponent>(_weaponComponent);
            container.AddService<HitPointsComponent>(_hitPointsComponent);
            container.AddService<TeamComponent>(_teamComponent);
            container.AddService<InputManager>(_inputManager);
        }
    }
}
