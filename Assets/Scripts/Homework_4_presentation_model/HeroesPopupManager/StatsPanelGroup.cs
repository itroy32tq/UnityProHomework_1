using Assets.Code.HeroesPopupManager;
using Lessons.Architecture.PM;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    public sealed class StatsPanelGroup : MonoBehaviour
    {
        [SerializeField] private List<StatPoint> _stats;

        private IStatsPresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        internal void SetAndSubscribeValue(IStatsPresenter statsPresenter)
        {
            _heroPresenter = statsPresenter;

            _heroPresenter.Stats.ObserveAdd().Subscribe(OnStatAdd).AddTo(_disposable);
            _heroPresenter.Stats.ObserveRemove().Subscribe(OnStatRemove).AddTo(_disposable);

            SetStatus(_heroPresenter.Stats);
        }

       
        private void OnStatValueChanged(CharacterStat stat)
        {
            for (int i = 0; i < _stats.Count; i++)
            {
                StatPoint item = _stats[i];

                if (item.Name == stat.Name)
                {
                    item.ValueLabel.text = stat.Value.ToString();
                }
            }
        }

        private void OnStatRemove(CollectionRemoveEvent<CharacterStat> removeEvent)
        {
            CharacterStat removeItem = removeEvent.Value;
            for (int i = 0; i < _stats.Count; i++)
            {
                StatPoint item = _stats[i];
                if (item.Name == removeItem.Name)
                {
                    item.gameObject.SetActive(false);
                    removeItem.OnValueChanged -= OnStatValueChanged;
                }
            }
        }

        private void OnStatAdd(CollectionAddEvent<CharacterStat> addEvent)
        {
            CharacterStat addItem = addEvent.Value;

            for (int i = 0; i < _stats.Count; i++)
            {
                StatPoint item = _stats[i];
                if (item.Name == addItem.Name)
                {
                    item.gameObject.SetActive(true);
                    item.ValueLabel.text = addItem.Value.ToString();

                    addItem.OnValueChanged += OnStatValueChanged;
                }
            }
            
        }

        private void SetStatus(IReadOnlyReactiveCollection<CharacterStat> list)
        {
            List<StatPoint> activeStats = list.Join(_stats,
                x => x.Name,
                y => y.Name,
                (x, y) =>
                {
                    y.gameObject.SetActive(true);

                    y.ValueLabel.text = x.Value.ToString();

                    x.OnValueChanged += OnStatValueChanged;
                    return y;

                }).ToList();
        }
    }
}
