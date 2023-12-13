using System.Collections.Generic;
using UnityEngine;

public abstract class VariantFireWeapon : MonoBehaviour
{
    protected BaseFireWeapons weapon;

    [field: Header("Settings weapon/missile")]
    [field: SerializeField] public List<Transform> PointsShot { get; protected set; }
    [field: SerializeField] public GameObject PrefabMissile { get; protected set; }
    [field: SerializeField] public Transform ParentContainerMissile { get; protected set; }

    [field: Header("Current specification of weapon/missile")]
    [field: SerializeField] public SpecificationFireWeapon TTXWeapon { get; protected set; }
    [field: SerializeField] public SpecificationMissile TTXMissile { get; protected set; }

    public virtual void InitializationWeapon(BaseFireWeapons weapon)
    {
        this.weapon = weapon;
    }

    public virtual void InitializationParametrs(SpecificationFireWeapon tTXWeapon, SpecificationMissile tTXMissile)
    {
        TTXWeapon = tTXWeapon;
        TTXMissile = tTXMissile;
    }
}
