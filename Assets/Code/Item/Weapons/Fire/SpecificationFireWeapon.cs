using UnityEngine;

namespace Ddd.Infrastructure
{
    [System.Serializable]
    public struct SpecificationFireWeapon
    {
        [Tooltip("Missile flight speed")] public float SpeedShot;
        [Tooltip("The spread of the shot relative to the PointsShot")] public float SpreadShot;

        [Space, Tooltip("Shooting by holding the key")] public bool AutoShooting;
        [Tooltip("Reload time after each shot")] public float CooldownTime;
    }
}