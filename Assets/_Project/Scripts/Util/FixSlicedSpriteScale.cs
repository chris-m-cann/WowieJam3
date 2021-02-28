using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class FixSlicedSpriteScale : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        private void Awake()
        {
            CorrectSpriteAndCollider();
        }

        [ContextMenu("CorrectSpriteAndCollider")]
        private void CorrectSpriteAndCollider()
        {

            _collider = GetComponent<BoxCollider2D>();
            _sprite = GetComponent<SpriteRenderer>();

            var scale = transform.localScale;
            _sprite.size = scale;
            _collider.size = scale;
            transform.localScale = Vector3.one;
        }

        [ContextMenu("ResetSprite")]
        private void ResetSprite()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.size = Vector2.one;
        }
    }
}