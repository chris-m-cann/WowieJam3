using System;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteColour : MonoBehaviour
    {
        public int ColourIndex;

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
            _sprite.color = palette.GetColour(ColourIndex);
            palette.OnChange -= SetUpSpriteColour;
            palette.OnChange += SetUpSpriteColour;
        }

        public void ChangeColour(int newColour)
        {
            ColourIndex = newColour;
            if (palette != null)
            {
                _sprite.color = palette.GetColour(ColourIndex);
            }
        }
    }
}