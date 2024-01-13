using Ddd.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Domain
{
    [CreateAssetMenu(menuName = "Scriptable object/Item/LaserWeapon")]
    public class ItemLaserWeaponData : ScriptableObject
    {
        [field: Header("Setting up an inventory slot for the item")]
        [field: SerializeField] public Sprite Sprite { get; private set; }

        [field: SerializeField, Space] public AnimationCurve AnimationTake { get; private set; }
        [field: SerializeField] public float TimeCorrectionPerMeterTake { get; private set; }
        [field: SerializeField] public float TimeCorrectionPerMeterThrow { get; private set; }

        [field: SerializeField, Space] public List<TypeSlot> InventorySlot { get; private set; }


        [field: Header("Current specification of weapon/missile")]
        [field: SerializeField] public int MaxLeavel { get; private set; } = 1;

        [field: Space, SerializeField] public SpecificationLasersWeapon TTXLaserWeapon { get; private set; }


        [field: Header("Improved specification of weapon/missile")]
        [field: SerializeField, Space] public List<ImprovementSpecificationLasersWeapon> ImprovementSpecificationsTTX { get; private set; }

        private void OnValidate()
        {
            MaxLeavel = ValidationData.OnValidateMaxLeavel(MaxLeavel);
            ImprovementSpecificationsTTX = ValidationData.OnValidateListImprovementSpecification(ImprovementSpecificationsTTX, MaxLeavel);
        }
    }
}