using Lessons.Architecture.PM;

namespace Assets.Code.HomeworksCode
{
    public sealed class HeroPresenterFactory
    {     
        public IHeroPresenter Create(HeroInfo heroInfo)
        {
            return new HeroPresenter(heroInfo);
        }
    }
}
