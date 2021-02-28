using System;
using System.ComponentModel;
using UnityEngine;
using Util;
using Util.Events;
using Wowie.Events;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail))]
    public class CircleTailRenderer: MonoBehaviour
    {
        [SerializeField] private ColourPalette palette;
        [SerializeField] private float alpha = .5f;
        [SerializeField] private SpriteRenderer head;
        [SerializeField] private BlockPickupGameEvent onPickup;


        private PlayerTail _tail;
        private int _lastIndex = -1;

        private void Awake()
        {
            _tail = GetComponent<PlayerTail>();
            onPickup.OnEventTrigger += OnPickup;
        }

        private void OnDestroy()
        {
            onPickup.OnEventTrigger -= OnPickup;
        }

        private void OnPickup(BlockPickup pickup)
        {
            if (pickup.ChangesLayer)
            {
                SetColour(pickup.ColourIndex);
            }
            else if (_lastIndex != -1)
            {
                SetColour(_lastIndex);
            }
        }

        public void SetColour(int idx)
        {

            var colour = palette.GetColour(idx);
            colour.a = alpha;

            head.color = colour;

            foreach (var block in _tail.Tail)
            {
                block.Background.color = colour;
            }

            _lastIndex = idx;
        }
    }
}