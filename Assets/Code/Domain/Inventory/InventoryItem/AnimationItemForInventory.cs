using Ddd.Application;
using System.Collections;
using UnityEngine;

namespace Ddd.Domain
{
    public class AnimationItemForInventory : MonoBehaviour
    {
        private const float PosYForDisconnectedItem = 0.1f;

        private float TimeAnimation(float correctionPerMeter, Vector3 endPosition) => Vector3.Distance(transform.position, endPosition)
            * correctionPerMeter;

        public IEnumerator MathAnimationTake(AnimationCurve intensity, float ñorrectionPerMeter)
        {
            var aroundSecond = TimeAnimation(ñorrectionPerMeter, Player.position);
            var startPosition = transform.position;

            for (var scaleTimer = Time.deltaTime / aroundSecond; scaleTimer < 1;
                scaleTimer = Mathf.Clamp01(scaleTimer + Time.deltaTime / aroundSecond))
            {
                transform.position = Vector3.Lerp(startPosition, Player.position, intensity.Evaluate(scaleTimer));
                yield return new WaitForEndOfFrame();
            }

            yield break;
        }

        public IEnumerator MathAnimationThrow(float correctionPerMeter)
        {
            var distanceY = transform.position.y - PosYForDisconnectedItem;
            var endPoint = transform.position + transform.forward * distanceY * 1.25f;
            endPoint.y = transform.position.y - distanceY;

            var aroundSecond = TimeAnimation(correctionPerMeter, endPoint);
            var startPosition = transform.position;

            for (var scaleTimer = Time.deltaTime / aroundSecond; scaleTimer < 1;
                scaleTimer = Mathf.Clamp01(scaleTimer + Time.deltaTime / aroundSecond))
            {
                transform.position = Vector3.Lerp(startPosition, endPoint, scaleTimer);
                yield return new WaitForEndOfFrame();
            }

            yield break;
        }
    }
}