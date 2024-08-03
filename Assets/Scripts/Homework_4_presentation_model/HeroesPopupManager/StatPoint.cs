using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.HeroesPopupManager
{
    public sealed class StatPoint : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Label { get; private set; }
        [field: SerializeField] public TMP_Text ValueLabel { get; private set; }
        [field: SerializeField] public Image Point { get; private set; }
        
        //todo для простоты просто хардкод на имя контрола
        public string Name => Label.name;


    }
}
