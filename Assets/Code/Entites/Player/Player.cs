using UnityEngine;

public class Player : AbstractEntity
{
    public static Vector3 position;

    [field: Header("Turn/Walk parameters")]
    [field: SerializeField] public float TurnDegreesInSecond { get; private set; } = 90f;
    [field: SerializeField] public float IgnoringRadiusTurn { get; private set; } = 1f;

    [field: Space, SerializeField] public float MaxSpeedWalk { get; private set; } = 4f;
    [field: SerializeField] public float SpeedInNewtons { get; private set; } = 500f;

    [field: Space, SerializeField, Range(0f, 1f)] public float CorrectorSpeedBack { get; private set; } = 0.64f;
    [field: SerializeField, Range(0f, 1f)] public float CorrectorSpeedRightOrLeft { get; private set; } = 0.8f;

    
    [field: Header("Autolooting parameters")]
    [field: SerializeField] public float RadiusAutoLooting { get; private set; } = 1.5f;
}