using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item/Other")]
public class ItemOtherData : ScriptableObject
{
    // refactoring
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Space] public AnimationCurve AnimationTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterThrow { get; private set; }

}