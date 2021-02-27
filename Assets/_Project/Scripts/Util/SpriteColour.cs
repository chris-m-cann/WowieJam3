using System;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteColour : MonoBehaviour
    {
        [SerializeField] private ColourPalette palette;
        [SerializeField] private int colourIndex;

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
            _sprite.color = palette.GetColour(colourIndex);
            palette.OnChange -= SetUpSpriteColour;
            palette.OnChange += SetUpSpriteColour;
        }

        public void ChangeColour(int newColour)
        {
            colourIndex = newColour;
            if (palette != null)
            {
                _sprite.color = palette.GetColour(colourIndex);
            }
        }
    }
}