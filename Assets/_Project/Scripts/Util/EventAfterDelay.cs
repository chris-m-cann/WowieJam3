using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class EventAfterDelay : MonoBehaviour
    {
        [SerializeField] private UnityEvent afterDelay;

        public void FireAfterDelay(float delay)
        {
            this.ExecuteAfter(delay, () => afterDelay.Invoke());
        }
    }
}