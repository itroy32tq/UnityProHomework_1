using Lessons.Architecture.PM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.HomeworksCode
{
    public sealed class ExperienceSlider : MonoBehaviour
    {
        [SerializeField] private Slider _experienceSlider;
        [SerializeField] private TMP_Text _experienceInfo;

        private IExperiencePresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        public void SetAndSubscribeValue(IExperiencePresenter heroPresenter)
        {
            _heroPresenter = heroPresenter;

            _heroPresenter.CurrentExperience.Subscribe(OnExperienceChanged).AddTo(_disposable);
        }

        private void OnExperienceChanged(int value)
        {
            _experienceSlider.value = (float)_heroPresenter.CurrentExperience.Value / _heroPresenter.RequiredExperience;
            _experienceInfo.text = $"XP: {_heroPresenter.CurrentExperience.Value}/{_heroPresenter.RequiredExperience}";
        }
    }
}
