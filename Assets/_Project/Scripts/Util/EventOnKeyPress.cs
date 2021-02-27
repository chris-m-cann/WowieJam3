using System;
using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class EventOnKeyPress : MonoBehaviour
    {
        [SerializeField] private UnityEvent onKeypress;
        [SerializeField] private KeyCode key = KeyCode.R;

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                onKeypress.Invoke();
            }
        }
    }
}