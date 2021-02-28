using System;
using TMPro;
using UnityEngine;

namespace Wowie
{
    [RequireComponent(typeof(TMP_Text))]
    public class SetScore : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            var tail = FindObjectOfType<PlayerTail>();
            if (tail != null)
            {
                _text.text = tail.Tail.Count.ToString();
            }
            else
            {
                _text.text = "2"; // random guess
            }
        }
    }
}