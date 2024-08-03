using Code;
using Lessons.Architecture.PM;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.HomeworksCode
{
    public sealed class LevelUpButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space]
        [SerializeField] private Image _buttonBackground;

        [SerializeField] private Sprite _availableButtonSprite;

        [SerializeField] private Sprite _lockedButtonSprite;

        private readonly CompositeDisposable _disposable = new();
        public Button Button => _button;

        public void SetAndSubscribeValue(IExperiencePresenter experiencePresenter)
        {

            experiencePresenter.CanLevelUpCommand.BindTo(Button).AddTo(_disposable);
            experiencePresenter.CanLevelUp.Subscribe(UpdateButtonState).AddTo(_disposable);
            UpdateButtonState(experiencePresenter.CanLevelUp.Value);
        }

        private void UpdateButtonState(bool canUpdate)
        {
            ButtonState buttonState = canUpdate
                ? ButtonState.Available
                : ButtonState.Locked;
            SetState(buttonState);
        }

        public void SetState(ButtonState state)
        {

            switch (state)
            {
                case ButtonState.Available:
                    Button.interactable = true;
                    _buttonBackground.sprite = _availableButtonSprite;
                    break;
                case ButtonState.Locked:
                    Button.interactable = false;
                    _buttonBackground.sprite = _lockedButtonSprite;
                    break;
                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
