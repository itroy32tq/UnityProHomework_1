using Lessons.Architecture.PM;
using System;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{

    public sealed class HeroPresenter : IHeroPresenter
    {
        private readonly HeroInfo _heroInfo;
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public IExperiencePresenter ExperiencePresenter { get; private set; }
        public IStatsPresenter StatsPresenter { get; private set; }
        
        public HeroPresenter(HeroInfo heroInfo)
        {
            _heroInfo = heroInfo;

            Name = _heroInfo.Name;
            Description = _heroInfo.Description;
            Icon = _heroInfo.Icon;
            ExperiencePresenter = new ExperiencePresenter(heroInfo);
            StatsPresenter = new StatsPresenter(heroInfo);
        }

        public void AddStat(CharacterStat stat)
        {
            StatsPresenter.AddStat(stat);
        }

        public CharacterStat GetStat(string name)
        {
            return StatsPresenter.GetStat(name);
        }

        public CharacterStat[] GetStats()
        {
            throw new NotImplementedException();
        }

        public void RemoveStat(CharacterStat stat)
        {
            StatsPresenter.RemoveStat(stat);
        }

        public void AddExperience(int range)
        {
           ExperiencePresenter.AddExperience(range);
        }

        public void LevelUp()
        {
            ExperiencePresenter.LevelUp();
        }
    }
}
