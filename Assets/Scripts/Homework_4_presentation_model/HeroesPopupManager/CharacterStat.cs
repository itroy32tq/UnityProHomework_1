using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }

        public Action<CharacterStat> OnValueChanged;

        public void SetValue(int value)
        { 
            Value = value;
            OnValueChanged?.Invoke(this);
        }
    }
}