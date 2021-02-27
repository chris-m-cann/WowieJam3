using UnityEngine;

namespace Util
{
    public static class GameObjectUtils
    {
        public static T GetComponentIfNotSet<T>(this GameObject self, T component)
        {
            if (component == null)
            {
                return self.GetComponent<T>();
            }

            return component;
        }

        public static void SetLayerInChildren(this GameObject self, int layer)
        {
            self.layer = layer;

            var children = self.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                var child = self.transform.GetChild(i);
                child.gameObject.SetLayerInChildren(layer);
            }
        }
    }
}