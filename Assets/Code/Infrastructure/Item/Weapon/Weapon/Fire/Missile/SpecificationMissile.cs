using UnityEngine;

namespace Ddd.Infrastructure
{
    [System.Serializable]
    public struct SpecificationMissile
    {
        [Tooltip("Base damage")] public float Damage;
        [Tooltip("Damage drop coefficient (1m/... %)"), Range(0f, 1f)] public float DamageDropCoefficient;

        [Space, Tooltip("The distance after which a reduction factor is applied to the base damage")] public float DistanceWithMaxDamage;
        [Tooltip("The distance after which the missile are deactivated")] public float MaxDistance;
    }
}