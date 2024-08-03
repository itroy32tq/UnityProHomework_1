using UnityEditor;
using UnityEngine;

namespace Code
{
    [CustomEditor(typeof(HeroHelper))]
    public sealed class HelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var lessonHelper = (HeroHelper) target;
            
            if (GUILayout.Button("ShowPopup"))
            {
                lessonHelper.ShowPopup();
            }
            
            if (GUILayout.Button("CreateHeroesPopup"))
            {
                lessonHelper.CreateHeroesPopup();
            }

            if (GUILayout.Button("ShowPopupInManager"))
            {
                lessonHelper.ShowPopupInManager();
            }

            if (GUILayout.Button("AddStat"))
            {
                lessonHelper.AddStat();
            }

            if (GUILayout.Button("AddExperience"))
            {
                lessonHelper.AddExperience();
            }

            if (GUILayout.Button("RemoveStat"))
            {
                lessonHelper.RemoveStat();
            }

            if (GUILayout.Button("GetStat"))
            {
                lessonHelper.GetStat();
            }

            if (GUILayout.Button("ChangeStatValue"))
            {
                lessonHelper.ChangeStatValue();
            }
        }
    }
}
