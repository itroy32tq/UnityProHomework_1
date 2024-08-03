using Lessons.Architecture.PM;
using System;
using UniRx;

namespace Assets.Code.HomeworksCode
{
    public sealed class ExperiencePresenter : IExperiencePresenter
    {
        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;
        private readonly ReactiveProperty<bool> _canLevelUp;
        private readonly CompositeDisposable _disposable = new();

        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;

        public int RequiredExperience => 100 * (CurrentLevel.Value + 1);

        public ReactiveCommand CanLevelUpCommand { get; }

        public ExperiencePresenter(HeroInfo heroInfo)
        {
            _currentLevel = new ReactiveProperty<int>(heroInfo.CurrentLevel);
            _currentExperience = new ReactiveProperty<int>(heroInfo.CurrentExperience);
            _canLevelUp = new ReactiveProperty<bool>(_currentExperience.Value == RequiredExperience);

            CanLevelUpCommand = new ReactiveCommand(_canLevelUp);
            CanLevelUpCommand.Subscribe(OnLevelUpCommand).AddTo(_disposable);
        }

        private void OnLevelUpCommand(Unit obj)
        {
            LevelUp();
        }

        public void AddExperience(int range)
        {
            int xp = Math.Min(CurrentExperience.Value + range, RequiredExperience);
            _currentExperience.Value = xp;
            _canLevelUp.Value = _currentExperience.Value == RequiredExperience;
        }

        public void LevelUp()
        {
            _currentExperience.Value = 0;
            _canLevelUp.Value = _currentExperience.Value == RequiredExperience;
            _currentLevel.Value++;
        }
    }
}
