using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item/LaserWeapon")]
public class ItemLaserWeaponData : ScriptableObject
{
    [field: Header("Setting up an inventory slot for the item")]
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Space] public AnimationCurve AnimationTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterThrow { get; private set; }

    [field: SerializeField, Space] public List<TypeSlot> InventorySlot { get; private set; }
}
