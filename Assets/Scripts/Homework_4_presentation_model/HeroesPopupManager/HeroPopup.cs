using Assets.Code.HomeworksCode;
using Code;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class HeroPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;

        [SerializeField] private LevelUpButton _levelUpButton;
        [SerializeField] private ExperienceSlider _experienceSlider;
        [SerializeField] private StatsPanelGroup _statsPanelGroup;

        [SerializeField] private Button _closeButton;

        private IHeroPresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        public void Show(IPresenter args)
        {
            if (args is not IHeroPresenter heroPresenter)
            {
                throw new ArgumentException("Invalid argument type");
            }

            Initialized(heroPresenter);
            
            gameObject.SetActive(true);
        }

        public void ShowFromManager()
        {
            if (_heroPresenter == null)
            {
                throw new ArgumentException("Invalid argument type");
            }

            gameObject.SetActive(true);
        }

        private void OnLevelChanged(int levelValue)
        {
            _level.text = levelValue.ToString();

        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
            _disposable.Clear();
        }

        internal void Initialized(IHeroPresenter heroPresenter)
        {
            _heroPresenter = heroPresenter;

            _name.text = heroPresenter.Name;
            _description.text = heroPresenter.Description;
            _icon.sprite = heroPresenter.Icon;

            _experienceSlider.SetAndSubscribeValue(heroPresenter.ExperiencePresenter);
            _statsPanelGroup.SetAndSubscribeValue(heroPresenter.StatsPresenter);
            _levelUpButton.SetAndSubscribeValue(heroPresenter.ExperiencePresenter);

            heroPresenter.ExperiencePresenter.CurrentLevel.Subscribe(OnLevelChanged);

            _closeButton.onClick.AddListener(Hide);

            gameObject.SetActive(false);
        }
    }   
}

