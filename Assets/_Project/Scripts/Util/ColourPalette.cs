using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(menuName = "Custom/ColourPallete")]
    public class ColourPalette : ScriptableObject
    {
        [SerializeField] private Color[] colours;
        public Color NotFound;

        public event Action OnChange;

        public Color GetColour(int index)
        {
            if (index < 0 || index >= colours.Length) return NotFound;

            return colours[index];
        }

        private void OnValidate()
        {
            OnChange?.Invoke();
        }
    }
}