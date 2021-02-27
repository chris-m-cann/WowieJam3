using System;
using Unity.Mathematics;
using UnityEngine;

namespace Wowie
{
    public class TailBlock : MonoBehaviour
    {
        public BlockPickup Pickup;
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
                transform.Rotate(0, 0, angle);
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
            Destroy(gameObject);
        }
    }
}