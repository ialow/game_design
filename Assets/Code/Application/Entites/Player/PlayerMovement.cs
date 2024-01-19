using Ddd.Domain;
using System;
using UnityEngine;

namespace Ddd.Application
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Player parameters;

        private float turnY;
        private float turnDegreesInSecond;
        private float ignoringRadiusTurn;

        private float maxSpeedWalk;

        private float speedInNewtons;
        private float correctorSpeedBack;
        private float correctorSpeedRightOrLeft;

        [SerializeField] private Animator animator;

        private Vector3 Walk
        {
            get
            {
                var derection = new Vector3(InputParametrs.ControllerDirectionXZ.x, 0f, InputParametrs.ControllerDirectionXZ.y).normalized;

                if (derection.z < 0) { derection.z *= correctorSpeedBack; }
                derection.x *= correctorSpeedRightOrLeft;

                return derection * speedInNewtons;
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            parameters = GetComponent<Player>();

            turnY = transform.rotation.y;
            InitializationParameters();
        }

        public void InitializationParameters()
        {
            turnDegreesInSecond = parameters.TurnDegreesInSecond;
            ignoringRadiusTurn = parameters.IgnoringRadiusTurn;

            maxSpeedWalk = parameters.MaxSpeedWalk;
            speedInNewtons = parameters.SpeedInNewtons;
            correctorSpeedBack = parameters.CorrectorSpeedBack;
            correctorSpeedRightOrLeft = parameters.CorrectorSpeedRightOrLeft;
        }

        private void FixedUpdate()
        {
            Move(Walk);
        }

        private void Update()
        {
            OffsetAngle();
        }

        private void Move(Vector3 walk)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.Euler(0f, turnY, 0f), turnDegreesInSecond * Time.fixedDeltaTime);

            if (rb.velocity.magnitude >= maxSpeedWalk)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedWalk);
                return;
            }

            rb.AddRelativeForce(walk, ForceMode.Force);
            animator.SetFloat("FrontMove", OnversionRange(new Vector2(rb.velocity.x, rb.velocity.z).magnitude, maxSpeedWalk));
        }

        private void OffsetAngle()
        {
            var playerPosXZ = new Vector2(transform.position.x, transform.position.z);
            var mousePosXZ = InputParametrs.MousePositionXZ;

            if (Vector2.Distance(mousePosXZ, playerPosXZ) > ignoringRadiusTurn)
            {
                var differenceBetweenCursorAndPlayerPosition = mousePosXZ - playerPosXZ;
                turnY = Mathf.Atan2(differenceBetweenCursorAndPlayerPosition.x, differenceBetweenCursorAndPlayerPosition.y) * Mathf.Rad2Deg;
            }
        }

        private float OnversionRange(float valueConverted, float inputRangeMax,
        float outputRangeMax = 1, float inputRangeMin = 0, float outputRangeMin = 0)
        {
            var diffOutputRange = MathF.Abs(outputRangeMax - outputRangeMin);
            var diffInputRange = MathF.Abs(inputRangeMax - inputRangeMin);
            var convFactor = (diffOutputRange / diffInputRange);
            return (outputRangeMin + (convFactor * (valueConverted - inputRangeMin)));
        }
    }
}