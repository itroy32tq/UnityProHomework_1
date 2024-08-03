using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    [CreateAssetMenu(fileName = "HeroesPool", menuName = "Data/New HeroesPool")]
    public sealed class HeroesPool : ScriptableObject
    {
        public HeroInfo[] Heroes;
    }
}
