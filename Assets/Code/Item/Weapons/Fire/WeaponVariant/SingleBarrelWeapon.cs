using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBarrelWeapon : VariantFireWeapon
{
    public override void InitializationWeapon(BaseFireWeapons weapon)
    {
        base.InitializationWeapon(weapon);
        weapon.SetVariantShotWeapon(ShotGun);
    }

    private void ShotGun(PoolObjects<GameObject> missile) => missile.ReturnActive(1);
}