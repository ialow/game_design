using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item/FireWeapon")]
public class ItemFireWeaponData : ScriptableObject
{
    [field: Header("Setting up an inventory slot for the item")]
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Space] public AnimationCurve AnimationTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterTake { get; private set; }
    [field: SerializeField] public float TimeCorrectionPerMeterThrow { get; private set; }

    [field: SerializeField, Space] public List<TypeSlot> InventorySlot { get; private set; }


    [field: Header("Current specification of weapon/missile")]
    [field: SerializeField] public SpecificationFireWeapon TTXWeapon { get; private set; }
    [field: SerializeField] public SpecificationMissile TTXMissile { get; protected set; }


    [field: Header("Improved specification of weapon/missile")]
    [field: SerializeField, Space] public List<ImprovementSpecificationFireWeapon> ImprovementSpecificationsTTX { get; private set; }
}