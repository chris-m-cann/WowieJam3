using System;
using UnityEngine;
using Util;

namespace Wowie
{
    public class BlockAnimation : MonoBehaviour
    {
        [SerializeField] private float breakScale;
        [SerializeField] private float breakTime;
        [SerializeField] private float enterTime;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private SpriteRenderer background;



        private Vector3 _initialScale;
        private Vector3 _scaledScale;
        private Color _color;
        private Color _backgroundColor;
        private void Awake()
        {
            _initialScale = transform.localScale;
            _scaledScale = _initialScale * breakScale;
        }

        public void OnBreak(Action onComplete)
        {
            onComplete = onComplete == null ? () => { } : onComplete;


            StartCoroutine(Tween.CoTweenVector3(_initialScale, _scaledScale, breakTime,
                scale => transform.localScale = scale, null, Tween.SmoothStop6));
            StartCoroutine(Tween.CoTweenFloat(1, 0, breakTime,
                a =>
                {
                    _color = sprite.color;
                    _color.a = a;
                    sprite.color = _color;
                }, null, Tween.SmoothStop6));

            _backgroundColor = background.color;

            StartCoroutine(Tween.CoTweenFloat(_backgroundColor.a, 0, breakTime,
                a =>
                {
                    _color = background.color;
                    _color.a = a;
                    background.color = _color;
                }, null, Tween.SmoothStop6));

            this.ExecuteAfter(breakTime, () => onComplete());
        }

        public void OnEnter(Action onComplete)
        {
            StartCoroutine(Tween.CoTweenVector3(Vector3.zero, _initialScale, enterTime,
                scale => transform.localScale = scale, onComplete, Tween.OutBack));
        }
    }
}