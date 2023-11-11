using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera orthographicCamera;

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
            var derection = new Vector3(InputParametrs.DiresctionWalk.x, 0f, InputParametrs.DiresctionWalk.y).normalized;

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
        var playerPos = transform.position;
        var posMouseVector3 = orthographicCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(new Vector2(posMouseVector3.x, posMouseVector3.z),
            new Vector2(playerPos.x, playerPos.z)) > ignoringRadiusTurn)
        {
            var differenceBetweenCursorAndPlayerPosition = posMouseVector3 - playerPos;
            turnY = Mathf.Atan2(differenceBetweenCursorAndPlayerPosition.x, differenceBetweenCursorAndPlayerPosition.z) * Mathf.Rad2Deg;
        }
    }
}