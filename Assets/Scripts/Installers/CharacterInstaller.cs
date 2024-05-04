using Assets.Scripts.InfroStructure;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class CharacterInstaller : Installer
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private CharacterConfig _characterConfig;

        [Listener] public Character Character = new();
        [Listener] public ShootEmUp.CharacterController CharacterController = new();

        public override void Install(DiContainer container)
        {
            container.AddService<CharacterConfig>(_characterConfig);
            container.AddService<GameManager>(_gameManager);
        }
    }
}
