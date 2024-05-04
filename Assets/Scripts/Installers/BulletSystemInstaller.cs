using Assets.Scripts.Bullets;
using Assets.Scripts.InfroStructure;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class BulletSystemInstaller : Installer
    {
        [SerializeField] private BulletSystemConfig _config;
        [SerializeField] private LevelBounds _levelBounds;

        [Listener] public BulletSystem BulletSystem = new();

        public override void Install(DiContainer container)
        {
            container.AddService<BulletSystemConfig>(_config);
            container.AddService<LevelBounds>(_levelBounds);
        }
    }
}
