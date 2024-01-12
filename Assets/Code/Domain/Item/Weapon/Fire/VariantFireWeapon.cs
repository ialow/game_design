using Ddd.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Domain
{
    public abstract class VariantFireWeapon : MonoBehaviour
    {
        protected BaseFireWeapons weapon;

        [field: Header("Settings weapon/missile")]
        [field: SerializeField] public List<Transform> PointsShot { get; protected set; }
        [field: SerializeField] public GameObject PrefabMissile { get; protected set; }
        [field: SerializeField] public Transform ParentContainerMissile { get; protected set; }

        public SpecificationFireWeapon TTXWeapon { get; protected set; }
        public SpecificationMissile TTXMissile { get; protected set; }

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
}