using System;
using UnityEngine;
using Util;
using Util.Events;
using Wowie.Events;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail))]
    public class PlayerCollisions: MonoBehaviour
    {
        [SerializeField] private Pair<int, string> layerMap;

        [SerializeField] private BlockPickupGameEvent onPickup;

        private int _layer = -1;
        private PlayerTail _tail;
        private int _playerLayer = -1;

        private void Awake()
        {
            _tail = GetComponent<PlayerTail>();
        }

        private void Start()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pickup"))
            {
                var pickup = other.gameObject.GetComponent<BlockPickup>();
                if (pickup == null) return;


                _tail.AddBlock(pickup.BlockPrefab, pickup);

                onPickup.Raise(pickup);
                ChangeLayer(pickup);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var block = other.gameObject.GetComponent<TailBlock>();

            if (block != null)
            {
                _tail.BreakAt(block);
                onPickup.Raise(block.Pickup);
                ChangeLayer(block.Pickup);
            }
        }

        private void ChangeLayer(BlockPickup pickup)
        {
            if (!pickup.ChangesLayer) return;

            var layer = LayerMask.NameToLayer(pickup.Layer);

            if (pickup.Layer.Length != 0 && layer != -1)
            {
                if (_layer != -1)
                {
                    Physics2D.IgnoreLayerCollision(_layer, _playerLayer, false);
                }
                Physics2D.IgnoreLayerCollision(layer, _playerLayer, true);
                _layer = layer;
            }
        }
    }
}