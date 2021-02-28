using System;
using UnityEngine;
using Util;

namespace Wowie
{
    public class GoalAnimation : MonoBehaviour
    {
        [SerializeField] private Shake shaker;

        [SerializeField] private Transform background;
        [SerializeField] private Transform circle;

        [SerializeField] private float maxCircleScale;
        [SerializeField] private float circleScaleDuration;
        [SerializeField] private float backgroundShakeDuration;
        [SerializeField] private float backgroundShakeMagnitude;
        [SerializeField] private float backgroundShakeDelay;


        private float _nextShake;

        private bool _tweenOutNextFrame = true;
        private bool _tweenInNextFrame = false;
        private Vector3 _initialCircleScale;
        private Vector3 _scaledCircleScale;
        private void Start()
        {
            _nextShake = Time.time + backgroundShakeDelay;
            _initialCircleScale = circle.localScale;
            _scaledCircleScale = _initialCircleScale * maxCircleScale;
        }

        private void Update()
        {
            Shake();

            Pulse();
        }

        private void Pulse()
        {
            if (_tweenOutNextFrame)
            {
                StopAllCoroutines();
                StartCoroutine(Tween.CoTweenVector3(_initialCircleScale, _scaledCircleScale, circleScaleDuration,
                    scale => circle.localScale = scale,
                    () => _tweenInNextFrame = true,
                    Tween.Lerp));
                _tweenOutNextFrame = false;
            }


            if (_tweenInNextFrame)
            {
                StartCoroutine(Tween.CoTweenVector3(_scaledCircleScale, _initialCircleScale, circleScaleDuration,
                    scale => circle.localScale = scale,
                    () => _tweenOutNextFrame = true,
                    Tween.Lerp));
                _tweenInNextFrame = false;
            }
        }

        private void Shake()
        {
            if (_nextShake < Time.time)
            {
                shaker.ShakeRotation(backgroundShakeMagnitude, backgroundShakeDuration, background);
                _nextShake = Time.time + backgroundShakeDuration + backgroundShakeDelay;
            }
        }
    }
}