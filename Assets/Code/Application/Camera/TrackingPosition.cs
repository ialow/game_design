using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Application
{
    public class TrackingPosition : MonoBehaviour
    {
        [SerializeField] private Transform targetPosition;
        [SerializeField] private float positionLag = 2f;

        private void LateUpdate()
        {
            PursuePositionXZ();
        }

        private void PursuePositionXZ()
        {
            var targetPosition = new Vector3(this.targetPosition.position.x, 0f, this.targetPosition.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionLag);
        }
    }
}