using System;
using System.ComponentModel;
using UnityEngine;
using Util;
using Util.Events;
using Wowie.Events;
using Void = Util.Void;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail))]
    public class CircleTailRenderer: MonoBehaviour
    {
        [SerializeField] private ColourPalette palette;
        [SerializeField] private float alpha = .5f;
        [SerializeField] private SpriteRenderer head;
        [SerializeField] private BlockPickupGameEvent onPickup;
        [SerializeField] private VoidGameEvent onBreak;


        private PlayerTail _tail;
        private int _lastIndex = -1;

        private void Awake()
        {
            _tail = GetComponent<PlayerTail>();
            onPickup.OnEventTrigger += OnPickup;
            onBreak.OnEventTrigger += OnBreak;
        }

        private void OnDestroy()
        {
            onPickup.OnEventTrigger -= OnPickup;
            onBreak.OnEventTrigger -= OnBreak;
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

        public void OnBreak(Void v)
        {
            if (_lastIndex != -1)
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