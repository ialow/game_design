using UnityEngine;

namespace Ddd.Infrastructure
{
    [System.Serializable]
    public struct ImprovementSpecificationFireWeapon
    {
        [field: SerializeField] public SpecificationFireWeapon TTXWeapon;
        [field: SerializeField] public SpecificationMissile TTXMissile;
    }
}