using GameEngine;
using Zenject;

namespace Assets.Scripts
{
    public sealed class LevelInstaller : MonoInstaller
    {
      
        public override void InstallBindings()
        {
            Container.
                Bind<IGameRepository>().
                To<GameRepository>().
                AsSingle().
                NonLazy();

            Container.
                Bind<ResourceService>().
                AsSingle().
                NonLazy();

            Container.
                Bind<UnitManager>().
                AsSingle().
                NonLazy();

            Container.
                Bind<ISaveLoader>().
                To<ResourcesSaveLoader>().
                AsSingle().
                NonLazy();

            Container.
                Bind<ISaveLoader>().
                To<UnitsSaveLoader>().
                AsSingle().
                NonLazy();
        }
    }
}
