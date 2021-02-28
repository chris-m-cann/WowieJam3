using System;
using UnityEngine;
using UnityEngine.Events;

namespace Wowie
{
    public class GoalBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEvent onComplete;

        private PlayerTail _tail;

        private void Start()
        {
            _tail = FindObjectOfType<PlayerTail>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var renderers = other.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            var blocks = other.GetComponentsInChildren<TailBlock>();
            foreach (var block in blocks)
            {
                block.ThroughGoal = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var block = other.GetComponent<TailBlock>();
            if (block != null && block == _tail.Tail[_tail.Tail.Count - 1])
            {
                onComplete.Invoke();
            }
        }
    }
}