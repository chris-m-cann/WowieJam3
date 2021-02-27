using UnityEngine;

namespace Wowie
{
    public class BlockPickup : MonoBehaviour
    {
        public TailBlock BlockPrefab;

        public void OnPickup()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }
    }
}