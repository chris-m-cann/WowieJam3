using System;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteColour : MonoBehaviour
    {
        public int ColourIndex;
        [Range(0, 1)]
        public float AlphaOverride = 1;

        [SerializeField] private ColourPalette palette;

        private SpriteRenderer _sprite;

        private void Awake()
        {
            SetUpSpriteColour();
        }

        private void OnValidate()
        {
            SetUpSpriteColour();
        }

        private void SetUpSpriteColour()
        {
            if (palette == null) return;
            _sprite = GetComponent<SpriteRenderer>();
            SetColour();
            palette.OnChange -= SetUpSpriteColour;
            palette.OnChange += SetUpSpriteColour;
        }

        private void SetColour()
        {
            var color = palette.GetColour(ColourIndex);
            color.a = AlphaOverride;
            _sprite.color = color;
        }

        public void ChangeColour(int newColour)
        {
            ColourIndex = newColour;
            if (palette != null)
            {
                SetColour();
            }
        }
    }
}