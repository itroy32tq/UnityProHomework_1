using Assets.Scripts.Bullets;
using Assets.Scripts.Level;
using ShootEmUp;
using UnityEngine;

namespace Assets.Scripts.InfroStructure
{
    public sealed class SceneInstaller : Installer
    {
        [SerializeField] private BulletSystemConfig _bulletSystemConfig;
        [SerializeField] private LevelBackgroundConfig _levelBackgroundConfig;
        [SerializeField] private LevelBoundsConfig _levelBoundsConfig;
        [SerializeField] private GameManager _gameManager;

        private readonly LevelBounds _levelBounds = new();

        [Listener] private readonly BulletSystem _bulletSystem = new();
        [Listener] private readonly LevelBackground _levelBackground = new();
        [Listener, SerializeField] private Bullet _bullet;

        public override void Install(DiContainer container)
        {
            container.AddService<BulletSystemConfig>(_bulletSystemConfig);
            container.AddService<LevelBackgroundConfig>(_levelBackgroundConfig);
            container.AddService<LevelBoundsConfig>(_levelBoundsConfig);

            container.AddService<LevelBounds>(_levelBounds);
            container.AddService<BulletSystem>(_bulletSystem);
            container.AddService<LevelBackground>(_levelBackground);
            container.AddService<GameManager>(_gameManager);
        }
    }
}
