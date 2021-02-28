using UnityEngine;
using Util;

namespace Wowie
{
    [RequireComponent(typeof(SpriteColour))]
    public class BlockPickup : MonoBehaviour
    {
        public TailBlock BlockPrefab;
        public int ColourIndex => _sprite.ColourIndex;
        public string Layer => BlockPrefab.Layer;
        public bool ChangesLayer => BlockPrefab.ChangesLayer;

        private SpriteColour _sprite;

        private void Awake()
        {
            _sprite = GetComponent<SpriteColour>();
        }

        public void OnPickup()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }
    }
}