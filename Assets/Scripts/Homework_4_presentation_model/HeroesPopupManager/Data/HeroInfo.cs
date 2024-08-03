using Lessons.Architecture.PM;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    [CreateAssetMenu(fileName = "HeroInfo", menuName = "Data/New HeroInfo")]
    public sealed class HeroInfo : ScriptableObject
    {
        [SerializeField] private Sprite _icon;

        [SerializeField] private string _name;
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _currentExperience;

        [Space]
        [SerializeField] private List<CharacterStat> _stats = new();
        [Space]
        [SerializeField][TextArea(3, 5)] private string _description;

        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public int CurrentLevel => _currentLevel;
        public int CurrentExperience => _currentExperience;

        public List<CharacterStat> Stats => _stats;
    }
}
