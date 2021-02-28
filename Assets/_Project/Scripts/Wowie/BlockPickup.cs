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

        [SerializeField] private PickupAnimation anims;
        [SerializeField] private Collider2D col;


        private SpriteColour _sprite;

        private void Awake()
        {
            _sprite = GetComponent<SpriteColour>();
        }

        public void OnPickup()
        {
            col.enabled = false;
            anims.Exit(() => gameObject.SetActive(false));
        }

        public void Reset()
        {
            gameObject.SetActive(true);
            anims.Enter(() => col.enabled = true);
        }
    }
}