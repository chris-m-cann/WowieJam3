using System;
using Unity.Mathematics;
using UnityEngine;

namespace Wowie
{
    public class TailBlock : MonoBehaviour
    {
        public BlockPickup Pickup;
        public string Layer;
        public bool ChangesLayer;
        public SpriteRenderer Background;
        public bool ThroughGoal = false;

        [SerializeField] private BlockAnimation anims;
        [SerializeField] private Collider2D col;

        private void Start()
        {
            col.enabled = false;
            anims.OnEnter(() => col.enabled = true);
        }

        public void Move(Transform connection, float spacing, float minDistance)
        {
            if (connection == null) return;

            var last = connection.position;
            var curr = transform.position;

            var dx = curr.x - last.x;
            var dy = curr.y - last.y;

            // calculate the angle between the two parts of the snake
            var angle = Mathf.Atan2(dy, dx);

            // get the new x and new y using polar coordinates
            var nx = spacing * Mathf.Cos(angle);
            var ny = spacing * Mathf.Sin(angle);

            // add the new x and new y to the last snake's position to "join" the two together without a gap
            curr.x = nx + last.x;
            curr.y = ny + last.y;

            if (Vector3.Distance(curr, transform.position) > minDistance)
            {
                transform.position = curr;

                var diff = last - curr;
                var a = Vector3.SignedAngle(transform.right, diff, Vector3.forward);

                transform.Rotate(0, 0, a);
            }
        }

        private void OnDestroy()
        {
            if (Pickup != null)
            {
                Pickup.Reset();
            }
        }

        public void OnBreakOff()
        {
            col.enabled = false;
            anims.OnBreak(() => Destroy(gameObject));
        }
    }
}