using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerParameters parameters;

    private float turnY;
    private float turnDegreesInSecond;
    private float ignoringRadiusTurn;

    private float maxSpeedWalk;

    private float speedInNewtons;
    private float correctorSpeedBack;
    private float correctorSpeedRightOrLeft;

    private Vector3 Walk
    {
        get
        {
            var derection = new Vector3(InputParametrs.ControllerPositionXZ.x, 0f, InputParametrs.ControllerPositionXZ.y).normalized;

            if (derection.z < 0) { derection.z *= correctorSpeedBack; }
            derection.x *= correctorSpeedRightOrLeft;

            return derection * speedInNewtons;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parameters = GetComponent<PlayerParameters>();

        turnY = transform.rotation.y;
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
}