using UnityEngine;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail))]
    public class PlayerCollisions: MonoBehaviour
    {
        private PlayerTail _tail;

        private void Awake()
        {
            _tail = GetComponent<PlayerTail>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pickup"))
            {
                var pickup = other.gameObject.GetComponent<BlockPickup>();
                if (pickup == null) return;


                _tail.AddBlock(pickup.BlockPrefab, pickup);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var block = other.gameObject.GetComponent<TailBlock>();

            if (block != null)
            {
                _tail.BreakAt(block);
            }
        }
    }
}