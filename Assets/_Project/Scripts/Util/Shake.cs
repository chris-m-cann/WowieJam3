using System.Collections;
using UnityEngine;

namespace Util
{
    public class Shake: MonoBehaviour
    {
        [Range(0, 360)] [SerializeField] private float maxAngleDegrees = 10f;
        [SerializeField] private float maxTranslation = .5f;
        [SerializeField] private float fequency = 2f;

        public void ShakeRotation(float magnitude, float duration, Transform target = null)
        {
            StartShake(magnitude, duration, shakePosition: false, shakeRotation: true, target);
        }

        public void ShakePosition(float magnitude, float duration, Transform target = null)
        {
            StartShake(magnitude, duration, shakePosition: true, shakeRotation: false, target);
        }

        public void ShakePositionAndRotation(float magnitude, float duration, Transform target = null)
        {
            StartShake(magnitude, duration, shakePosition: true, shakeRotation: true, target);
        }

        public void StartShake(float magnitude, float duration, bool shakePosition, bool shakeRotation,
            Transform target = null)
        {
            var t = target == null ? transform : target;

            StartCoroutine(CoShake(magnitude, duration, t, shakePosition, shakeRotation));
        }

        private IEnumerator CoShake(float magnitude, float duration, Transform target, bool shakePosition, bool shakeRotation)
        {
            float start = Time.time;
            float end = start + duration;

            float lastDeltaAngle = 0f;
            Vector3 lastDeltaTranslation = Vector3.zero;

            var seed = Random.Range(1f, 100f);

            while (end > Time.time && target != null)
            {
                if (target == null) yield break;

                var t = (Time.time - start) / duration;
                var m = Tween.SmoothStop3(magnitude, 0, t);

                if (shakePosition)
                {

                    var deltaX = maxTranslation * m * GetFloatOverTime(seed * 10);
                    var deltaY = maxTranslation * m * GetFloatOverTime(seed * 100);
                    var translation = new Vector3(deltaX, deltaY, 0f);
                    target.position -= lastDeltaTranslation;
                    target.position += translation;
                    lastDeltaTranslation = translation;
                }


                if (shakeRotation)
                {
                    var deltaAngle = maxAngleDegrees * m * GetFloatOverTime(seed);
                    var eulers = target.rotation.eulerAngles;
                    // return to normal
                    eulers.z -= lastDeltaAngle;
                    eulers.z += deltaAngle;

                    target.rotation = Quaternion.Euler(eulers);

                    lastDeltaAngle = deltaAngle;
                }

                yield return null;
            }

            if (target == null) yield break;

            // return to normal
            if (shakePosition)
            {
                target.position -= lastDeltaTranslation;
            }

            if (shakeRotation)
            {
                var finalEulers = target.rotation.eulerAngles;
                finalEulers.z -= lastDeltaAngle;
                target.rotation = Quaternion.Euler(finalEulers);
            }
        }

        private float GetFloatOverTime(float seed)
        {
            var p = Mathf.Clamp01(Mathf.PerlinNoise(seed, Time.time  * fequency));
            // start of range + p * bredth of range (-1, 1) in this case
            return -1 + (p * 2);
        }
    }
}