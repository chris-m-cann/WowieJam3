using System;
using System.Collections;
using UnityEngine;

namespace Util
{
 public static class Tween
    {
        public delegate float Function(float start, float end, float time);
        public static float Lerp(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t;
        }

        #region SmoothStart

        public static float SmoothStart2(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t * t;
        }

        public static float SmoothStart3(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t * t * t;
        }

        public static float SmoothStart4(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t * t * t * t;
        }

        public static float SmoothStart5(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t * t * t * t * t;
        }

        public static float SmoothStart6(float start, float end, float time)
        {
            var t = Mathf.Clamp01(time);
            return start + (end - start) * t * t * t * t * t * t;
        }

        #endregion

        #region SmoothStop

        public static float SmoothStop2(float start, float end, float time)
        {
            var t =  1 - Mathf.Clamp01(time);
            return start + (end - start) * (1 - t * t);
        }

        public static float SmoothStop3(float start, float end, float time)
        {
            var t =  1 - Mathf.Clamp01(time);
            return start + (end - start) * (1 - t * t * t);
        }

        public static float SmoothStop4(float start, float end, float time)
        {
            var t =  1 - Mathf.Clamp01(time);
            return start + (end - start) * (1 - t * t * t * t);
        }

        public static float SmoothStop5(float start, float end, float time)
        {
            var t =  1 - Mathf.Clamp01(time);
            return start + (end - start) * (1 - t * t * t * t * t);
        }

        public static float SmoothStop6(float start, float end, float time)
        {
            var t =  1 - Mathf.Clamp01(time);
            return start + (end - start) * (1 - t * t * t * t * t * t);
        }

        #endregion

        #region SmoothStep

        public static float SmoothStep3(float start, float end, float time)
        {
            var t =  Mathf.Clamp01(time);
            var inT = t * t;
            var outT = 1 - (1 - t) * (1 - t);

            var fade = (1 - t) * inT + t * outT;

            return start + (end - start) * fade;
        }

        public static float SmoothStep4(float start, float end, float time)
        {
            var t =  Mathf.Clamp01(time);
            var inT = t * t * t;
            var outT = 1 - (1 - t) * (1 - t) * (1 - t);

            var fade = (1 - t) * inT + t * outT;

            return start + (end - start) * fade;
        }

        public static float SmoothStep5(float start, float end, float time)
        {
            var t =  Mathf.Clamp01(time);
            var inT = t * t * t * t;
            var outT = 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);

            var fade = (1 - t) * inT + t * outT;

            return start + (end - start) * fade;
        }

        public static float SmoothStep6(float start, float end, float time)
        {
            var t =  Mathf.Clamp01(time);
            var inT = t * t * t * t * t;
            var outT = 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t) * (1 - t);

            var fade = (1 - t) * inT + t * outT;

            return start + (end - start) * fade;
        }

        #endregion


        #region others


        public static float OutBack(float start, float end, float time)
        {
            float s = 1.70158f;
            end -= start;
            time = (time) - 1;
            return end * ((time) * time * ((s + 1) * time + s) + 1) + start;
        }

        public static float InBack(float start, float end, float time)
        {
            end -= start;
            time /= 1;
            float s = 1.70158f;
            return end * (time) * time * ((s + 1) * time - s) + start;
        }


        #endregion

        public static IEnumerator CoTweenFloat(
            float from, float to, float time,
            Action<float> update, Action onComplete,
            Func<float, float, float, float> tweenFun
        )
        {
            onComplete = onComplete == null ? () => { } : onComplete;
            var start = Time.time;
            var duration = time;
            var end = start + duration;
            float current = from;

            while (Time.time < end)
            {
                var t = (Time.time - start) / duration;
                current = tweenFun(from, to, t);
                update.Invoke(current);

                yield return null;
            }

            update.Invoke(to);
            onComplete.Invoke();
        }

        public static IEnumerator CoLerpPosition(Transform target, Vector3 from, Vector3 to, float time, Action onComplete)
        {
            Vector3 endPoint = to;

            var startPoint = from;
            var start = Time.time;
            var duration = time;
            var end = start + duration;

            while (Time.time < end)
            {
                var t = (Time.time - start) / duration;
                var x = Tween.Lerp(startPoint.x, endPoint.x, t);
                var y = Tween.Lerp(startPoint.y, endPoint.y, t);
                var z = Tween.Lerp(startPoint.z, endPoint.z, t);
                target.position = new Vector3(x, y, z);

                yield return null;
            }

            target.position = to;
            onComplete?.Invoke();
        }

        public static IEnumerator CoSmoothStep6Position(Transform target, Vector3 from, Vector3 to, float time, Action onComplete)
        {
            Vector3 endPoint = to;

            var startPoint = from;
            var start = Time.time;
            var duration = time;
            var end = start + duration;

            while (Time.time < end)
            {
                var t = (Time.time - start) / duration;
                var x = Tween.SmoothStep6(startPoint.x, endPoint.x, t);
                var y = Tween.SmoothStep6(startPoint.y, endPoint.y, t);
                var z = Tween.SmoothStep6(startPoint.z, endPoint.z, t);
                target.position = new Vector3(x, y, z);

                yield return null;
            }

            target.position = to;
            onComplete?.Invoke();
        }

        public static IEnumerator CoTweenVector3(
            Vector3 from, Vector3 to, float time,
            Action<Vector3> update, Action onComplete,
            Func<float, float, float, float> tweenFun
            )
        {
            onComplete = onComplete == null ? () => { } : onComplete;
            Vector3 endPoint = to;

            var startPoint = from;
            var start = Time.time;
            var duration = time;
            var end = start + duration;
            Vector3 current = from;

            while (Time.time < end)
            {
                var t = (Time.time - start) / duration;
                current.x = tweenFun(startPoint.x, endPoint.x, t);
                current.y = tweenFun(startPoint.y, endPoint.y, t);
                current.z = tweenFun(startPoint.z, endPoint.z, t);
                update.Invoke(current);

                yield return null;
            }

            update.Invoke(to);

            yield return null;
            onComplete.Invoke();
        }

        public static IEnumerator CoLerpVector3(
            this MonoBehaviour self,
            Vector3 from, Vector3 to, float time,
            Action<Vector3> update, Action onComplete)
        {
            yield return self.StartCoroutine(CoTweenVector3(from, to, time, update, onComplete, Tween.Lerp));
        }

        public static IEnumerator CoTweenRotation(
            this MonoBehaviour self,
            Transform transform,
            Vector3 from, Vector3 to, float time,
            Action onComplete,
            Func<float, float, float, float> tweenFun
        )
        {
            yield return self.StartCoroutine(CoTweenVector3(from, to, time, update: v3 => transform.rotation = Quaternion.Euler(v3), onComplete, tweenFun));
        }
    }
}