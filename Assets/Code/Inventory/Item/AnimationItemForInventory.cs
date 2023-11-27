using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationItemForInventory : MonoBehaviour
{
    public IEnumerator MathAnimationTake(AnimationCurve intensity, float second)
    {
        var time = 0f;
        var startPosition = transform.position;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, PlayerParameters.position, intensity.Evaluate(time));
            time += Mathf.Clamp01(Time.deltaTime / second);
            yield return new WaitForEndOfFrame();
        }
    }
}