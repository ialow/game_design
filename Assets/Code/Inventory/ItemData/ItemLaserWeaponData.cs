using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item/LaserWeapon")]
public class ItemLaserWeaponData : ScriptableObject
{
    // refactoring
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Space] public AnimationCurve AnimationTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterThrow { get; private set; }
}
