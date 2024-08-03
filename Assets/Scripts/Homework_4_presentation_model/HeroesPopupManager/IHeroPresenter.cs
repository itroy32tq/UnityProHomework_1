using Code;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IHeroPresenter : IPresenter
    {
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        IExperiencePresenter ExperiencePresenter { get; }
        IStatsPresenter StatsPresenter { get; }
        void AddStat(CharacterStat stat);
        void RemoveStat(CharacterStat stat);
        CharacterStat GetStat(string name);
        CharacterStat[] GetStats();
        void LevelUp();
        void AddExperience(int range);
    }

    public interface IExperiencePresenter : IPresenter
    {
        IReadOnlyReactiveProperty<int> CurrentLevel { get; }
        IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        int RequiredExperience { get; }
        ReactiveCommand CanLevelUpCommand { get; }
        void LevelUp();
        void AddExperience(int range);
    }

    public interface IStatsPresenter : IPresenter
    {
        IReadOnlyReactiveCollection<CharacterStat> Stats { get; }
        void AddStat(CharacterStat stat);
        void RemoveStat(CharacterStat stat);
        CharacterStat GetStat(string name);
        CharacterStat[] GetStats();
    }

}
