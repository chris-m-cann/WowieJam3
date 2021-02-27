using System;
using UnityEngine;
using Util;
using Util.Events;

namespace Wowie
{
    [RequireComponent(typeof(PlayerTail), typeof(LineRenderer))]
    public class TailRenderer: MonoBehaviour
    {
        [SerializeField] private ColourPalette palette;
        [SerializeField] private float alpha = .5f;



        private PlayerTail _tail;
        private LineRenderer _renderer;

        private Vector3[] positions = new Vector3[10];
        [SerializeField] private float extra;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
            _tail = GetComponent<PlayerTail>();
        }

        private void Update()
        {
            var size = _tail.Tail.Count;
            if (size < 2)
            {
                _renderer.positionCount = 0;
                return;
            }

            if (size + 2 > positions.Length)
            {
                Array.Resize(ref positions, size + 2);
            }

            for (int i = 0; i < size; i++)
            {
                positions[i + 1] = _tail.Tail[i].transform.position;
            }

            positions[0] = transform.position + (transform.right * extra);
            var last = _tail.Tail[size - 1];
            positions[size + 1] = last.transform.position - (last.transform.right * extra);

            _renderer.positionCount = size + 1;
            _renderer.SetPositions(positions);
        }

        public void SetColour(int idx)
        {

            var colour = palette.GetColour(idx);
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(colour, 0.0f), new GradientColorKey(colour, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );

            _renderer.colorGradient = gradient;
        }
    }
}