using Assets.Code.HeroPopupManager;
using Assets.Code.HomeworksCode;
using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class HeroHelper : MonoBehaviour
    {
        [SerializeField] private HeroInfo _heroInfo; 
        [SerializeField] private HeroesPool _heroesPool;

        [SerializeField] private Transform _container;
        [SerializeField] private int _range;

        private HeroesPopupManager _managerPrefab;
        private HeroPopup _heroPrefab;

        private HeroesPopupManager _heroesPopupManager;

        private HeroPresenterFactory _factory;
        private IHeroPresenter _currentPresenter;
        private HeroPopup _currentPopup;

        [SerializeField] private CharacterStat _characterStat;
        [SerializeField] private string _characterStatName;
        [SerializeField] private int _value;

        [Inject]
        private void Construct(HeroPresenterFactory presentorFactory, HeroesPopupManager heroesPopupManager, HeroPopup heroPopup)
        {
            _factory = presentorFactory;
            _heroPrefab = heroPopup;
            _managerPrefab = heroesPopupManager;
        }

        public void ShowPopup()
        {
            if (_heroInfo == null)
            {
                Debug.Log("отсутствуют данные для формирования презентера");
                return;
            }

            _currentPresenter = _factory.Create(_heroInfo);

            if (_currentPopup == null)
            {
                _currentPopup = Instantiate(_heroPrefab, _container);

            }
            else
            {
                Destroy(_currentPopup.gameObject);
                _currentPopup = Instantiate(_heroPrefab, _container);
            }

            _currentPopup.Show(_currentPresenter);
        }

        public void CreateHeroesPopup()
        {
            if (_currentPopup != null)
            {
                Destroy(_currentPopup.gameObject);
            }

            if (_heroesPopupManager == null)
            {
                _heroesPopupManager = Instantiate(_managerPrefab, _container);

            }
            else 
            {
                Destroy(_heroesPopupManager.gameObject);
                _heroesPopupManager = Instantiate(_managerPrefab, _container);
            }

            _heroesPopupManager.Container = _container;
            _heroesPopupManager.Create(new HeroesPopupPresenter(_heroesPool, _factory));
        }

        public void ShowPopupInManager()
        {
            _heroesPopupManager.Show();
        }

        public void AddExperience()
        {
            _currentPresenter.AddExperience(_range);
        }

        public void AddStat()
        {
            _currentPresenter.AddStat(_characterStat);
        }

        public void RemoveStat()
        {
            _currentPresenter.RemoveStat(_characterStat);
        }

        public void GetStat()
        {
            _characterStat = _currentPresenter.GetStat(_characterStatName);
        }

        public void ChangeStatValue()
        {
            _characterStat = _currentPresenter.GetStat(_characterStatName);
            _characterStat.SetValue(_value);
        }
    }
}
