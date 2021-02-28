using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class CircleEdgeCollider : MonoBehaviour
    {
        [SerializeField] private int segments = 10;
        [SerializeField] private float radius = 1;

        private EdgeCollider2D _collider;


        private void OnValidate()
        {
            CreateCollider();
        }

        [ContextMenu("GenerateCollider")]
        private void CreateCollider()
        {
            _collider = GetComponent<EdgeCollider2D>();
            if (_collider == null || segments < 2) return;

            var inc = 2 * Mathf.PI / segments;
            var points = new List<Vector2>(segments);

            float angle;
            float x;
            float y;

            for (int i = 0; i < segments; ++i)
            {
                angle = inc * i;
                x = radius * Mathf.Sin(angle);
                y = radius * Mathf.Cos(angle);

                points.Add(new Vector2(x, y));
            }

            // add link back to beginning
            x = radius * Mathf.Sin(0);
            y = radius * Mathf.Cos(0);

            points.Add(new Vector2(x, y));


            points.Add(new Vector2(x, y));

            _collider.SetPoints(points);
        }
    }
}