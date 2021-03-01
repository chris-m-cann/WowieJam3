using System;
using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class EventAfterDelay : MonoBehaviour
    {
        [SerializeField] private bool runOnStart;
        [SerializeField] private float delay;
        [SerializeField] private UnityEvent afterDelay;

        private void Start()
        {
            if (runOnStart)
            {
                FireAfterDelay(delay);
            }
        }

        public void FireAfterDelay(float delay)
        {
            this.ExecuteAfter(delay, () => afterDelay.Invoke());
        }
    }
}