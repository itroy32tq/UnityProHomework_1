using Zenject;

namespace Assets.Code.HomeworksCode
{
    public sealed class HeroInstaller
    {
        public HeroInstaller(DiContainer container)
        {
            container.Bind<HeroPresenterFactory>().AsSingle().NonLazy();
        }
    }
}
