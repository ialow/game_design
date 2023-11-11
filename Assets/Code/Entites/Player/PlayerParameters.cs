using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    [field: Header("Turn/Walk parameters")]
    [field: SerializeField] public float TurnDegreesInSecond { get; private set; } = 90f;
    [field: SerializeField] public float IgnoringRadiusTurn { get; private set; } = 1f;
    
    [field: Space, SerializeField] public float MaxSpeedWalk { get; private set; }  = 4f;
    [field: SerializeField] public float SpeedInNewtons { get; private set; } = 8f;

    [field: Space, SerializeField, Range(0f, 1f)] public float CorrectorSpeedBack { get; private set; } = 0.944f;
    [field: SerializeField, Range(0f, 1f)] public float CorrectorSpeedRightOrLeft { get; private set; } = 0.970f;
}