using Lessons.Architecture.PM;
using System;
using UniRx;

namespace Assets.Code.HomeworksCode
{
    public sealed class StatsPresenter : IStatsPresenter
    {
        private readonly ReactiveCollection<CharacterStat> _stats;

        private readonly CompositeDisposable _disposable = new();

        public IReadOnlyReactiveCollection<CharacterStat> Stats => _stats;

        public StatsPresenter(HeroInfo heroInfo)
        {
            _stats = new ReactiveCollection<CharacterStat>(heroInfo.Stats);
        }

        public void AddStat(CharacterStat stat)
        {
            CharacterStat target = GetStat(stat.Name);

            if (target == null)
            {
                _stats.Add(stat);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (CharacterStat stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            return null;
        }

        public CharacterStat[] GetStats()
        {
            throw new NotImplementedException();
        }

        public void RemoveStat(CharacterStat stat)
        {
            CharacterStat target = GetStat(stat.Name);

            if (target != null)
            {
                _stats.Remove(target);
            }
        }
    }
}
