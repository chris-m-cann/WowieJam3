using System;
using System.ComponentModel;
using UnityEngine;
using Util;
using Util.Events;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail))]
    public class CircleTailRenderer: MonoBehaviour
    {
        [SerializeField] private ColourPalette palette;
        [SerializeField] private float alpha = .5f;
        [SerializeField] private SpriteRenderer head;

        private PlayerTail _tail;

        private void Awake()
        {
            _tail = GetComponent<PlayerTail>();
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
        }
    }
}