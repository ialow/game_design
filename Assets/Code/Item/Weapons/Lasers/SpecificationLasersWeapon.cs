using UnityEngine;

namespace Ddd.Infrastructure
{
    [System.Serializable]
    public struct SpecificationLasersWeapon
    {
        [Tooltip("Damage")] public float DamagePreSecond;
        [Tooltip("Distance")] public float MaxDistance;
    }
}