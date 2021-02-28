using System;
using UnityEngine;

namespace Wowie
{
    public class SceneInit : MonoBehaviour
    {
        private void Awake()
        {
            var pink = LayerMask.NameToLayer("Pink");
            var blue = LayerMask.NameToLayer("Blue");
            var green = LayerMask.NameToLayer("Green");
            var player = LayerMask.NameToLayer("Player");

            Physics2D.IgnoreLayerCollision(player, pink, false);
            Physics2D.IgnoreLayerCollision(player, blue, false);
            Physics2D.IgnoreLayerCollision(player, green, false);
        }
    }
}