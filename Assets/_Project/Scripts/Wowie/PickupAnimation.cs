using System;
using System.Collections;
using UnityEngine;
using Util;

namespace Wowie
{
    public class PickupAnimation : MonoBehaviour
    {
        [SerializeField] private Transform background;
        [SerializeField] private Transform foreground;

        [SerializeField] private float maxScaleFactor;
        [SerializeField] private float scaleDurationIn;
        [SerializeField] private float scaleDurationOut;


        [SerializeField] private float tweenInTime = .5f;
        [SerializeField] private float tweenOutTime = .5f;


        private bool _tweenOutNextFrame = true;
        private bool _tweenInNextFrame = false;
        private Vector3 _initialBackgroundScale;
        private Vector3 _largeScale;
        private bool _pulsing = false;

        private Vector3 _initialScale;


        private void Start()
        {
            _initialScale = transform.localScale;
            _initialBackgroundScale = background.localScale;
            _largeScale = _initialBackgroundScale * maxScaleFactor;

            Enter(null);
        }

        private void Update()
        {
            if (_pulsing)
            {
                Pulse(background, _initialBackgroundScale, _largeScale, scaleDurationIn, scaleDurationOut);
            }
        }

        public void Enter(Action onComplete)
        {
            StopAllCoroutines();
            StartCoroutine(CoEnterAnim(onComplete));
        }

        public void Exit(Action onComplete)
        {
            StopAllCoroutines();
            StartCoroutine(CoExitAnim(onComplete));
        }

        private IEnumerator CoEnterAnim(Action onComplete)
        {
            onComplete = onComplete == null ? () => { } : onComplete;
            yield return StartCoroutine(Tween.CoTweenVector3(Vector3.zero, _initialScale, tweenInTime,
                scale => transform.localScale = scale, null, Tween.OutBack));

            yield return null;

            _pulsing = true;
            onComplete.Invoke();
            yield return null;
        }


        private IEnumerator CoExitAnim(Action onComplete)
        {
            _pulsing = false;
            onComplete = onComplete == null ? () => { } : onComplete;
            yield return StartCoroutine(Tween.CoTweenVector3(_initialScale , Vector3.zero, tweenOutTime,
                scale => transform.localScale = scale, null, Tween.InBack));

            yield return null;

            onComplete.Invoke();
            yield return null;
        }

        private void Pulse(Transform target, Vector3 small, Vector3 big, float inDuration, float outDuration)
        {
            if (_tweenOutNextFrame)
            {
                StopAllCoroutines();
                StartCoroutine(Tween.CoTweenVector3(small, big, outDuration,
                    scale => target.localScale = scale,
                    () => _tweenInNextFrame = true,
                    Tween.Lerp));
                _tweenOutNextFrame = false;
            }


            if (_tweenInNextFrame)
            {
                StartCoroutine(Tween.CoTweenVector3(big, small, inDuration,
                    scale => target.localScale = scale,
                    () => _tweenOutNextFrame = true,
                    Tween.Lerp));
                _tweenInNextFrame = false;
            }
        }
    }
}