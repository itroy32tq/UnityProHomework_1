using Code;
using Lessons.Architecture.PM;
using System.Collections.Generic;

namespace Assets.Code.HeroPopupManager
{
    public interface IHeroPopupPresenter : IPresenter
    {
        IReadOnlyList<IHeroPresenter> HeroPresenters { get; }
    }
}
