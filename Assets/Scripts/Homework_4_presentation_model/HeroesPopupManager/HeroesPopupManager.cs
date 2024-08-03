using Assets.Code.HeroPopupManager;
using Lessons.Architecture.PM;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class HeroesPopupManager : MonoBehaviour
    {
        private Transform _container; 
        [SerializeField] private HeroPopup _heroPopupPrefab;
        [SerializeField] private Button _hideButton;

        int _selectedIndex = 0;
    
        private readonly List<HeroPopup> _views = new();

        public Transform Container { get => _container; set => _container = value; }

        public void Create(IPresenter args)
        {
            if (args is not IHeroPopupPresenter heroPopupPresenter)
            {
                throw new Exception("Expected IShopPopupPresenter");
            }

            gameObject.SetActive(true);

            for (var index = 0; index < heroPopupPresenter.HeroPresenters.Count; index++)
            {
                IHeroPresenter productPresenter = heroPopupPresenter.HeroPresenters[index];
                HeroPopup view = Instantiate(_heroPopupPrefab, Container);
                view.Initialized(productPresenter);
                _views.Add(view);
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            for (var index = 0; index < _views.Count; index++)
            {
                HeroPopup productView = _views[index];
                Destroy(productView.gameObject);
            }
            _views.Clear();
            _hideButton.onClick.RemoveListener(Hide);
        }

        internal void Show()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                HeroPopup view = _views[i];
                view.Hide();
            }

            if (_selectedIndex > _views.Count - 1)
            {
                _selectedIndex = 0;
            }

            _views[_selectedIndex].ShowFromManager();
            _selectedIndex++;
        }
    }
}
