using Ddd.Infrastructure;
using UnityEngine;

namespace Ddd.Domain
{
    public class SingleBarrelWeapon : VariantFireWeapon
    {
        public override void InitializationWeapon(BaseFireWeapons weapon)
        {
            base.InitializationWeapon(weapon);
            weapon.SetVariantShotWeapon(SingleBarrelShot);
        }

        private void SingleBarrelShot(PoolObjects<GameObject> missile) => missile.ReturnActive(1);
    }
}