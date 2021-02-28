using System;
using UnityEditor;
using UnityEngine;
using Util;
using Wowie.Events;

namespace Wowie
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class BlockerController : MonoBehaviour
    {
        [SerializeField] private BlockPickupGameEvent onPickup;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private float alpha;
        [SerializeField] private float tweenTime = 1f;


        private SpriteRenderer _sprite;
        private Collider2D _collider;
        private PlayerTail _playerTail;


        private Collider2D[] _overlapResults = new Collider2D[10];

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _sprite = GetComponent<SpriteRenderer>();

            onPickup.OnEventTrigger += OnPickup;
        }

        private void Start()
        {
            _playerTail = FindObjectOfType<PlayerTail>();
        }

        private void OnDestroy()
        {
            onPickup.OnEventTrigger -= OnPickup;
        }


        private void OnPickup(BlockPickup pickup)
        {
            if (!pickup.ChangesLayer) return;


            var layerName = LayerMask.LayerToName(gameObject.layer);
            if (pickup.Layer == layerName)
            {
                ChangeAlpha(alpha, null);
            }
            // if we were the correct layer then chop off any blocks in my vacinity
            else if (_sprite.color.a < 1)
            {
                _collider.enabled = false;
                ChangeAlpha(1, () =>
                {
                    _collider.enabled = true;
                    // EditorApplication.isPaused = true;
                    var filter = new ContactFilter2D();
                    filter.layerMask = playerMask;
                    var colliders = Physics2D.OverlapCollider(_collider, filter, _overlapResults);

                    int idx = int.MaxValue;
                    for (var i = 0; i < colliders; i++)
                    {
                        var block = _overlapResults[i].GetComponent<TailBlock>();
                        if (block != null)
                        {
                            var blockIdx = _playerTail.Tail.IndexOf(block);
                            if (blockIdx > -1 && blockIdx < idx)
                            {
                                idx = blockIdx;
                            }
                        }
                    }

                    if (idx != int.MaxValue)
                    {
                        _playerTail.BreakAt(idx);
                    }
                });
            }
        }

        private void ChangeAlpha(float targetAlpha, Action onComplete)
        {
            StopAllCoroutines();
            StartCoroutine(Tween.CoTweenFloat(_sprite.color.a, targetAlpha, tweenTime, a =>
            {
                var color = _sprite.color;
                color.a = a;
                _sprite.color = color;
            }, onComplete, Tween.Lerp));
        }
    }
}