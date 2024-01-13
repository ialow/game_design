using Ddd.Infrastructure;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ddd.Domain
{
    [RequireComponent(typeof(VariantFireWeapon))]
    public abstract class BaseFireWeapons : MonoBehaviour, IShootingable
    {
        protected VariantFireWeapon variantFireWeapon;

        protected Action<PoolObjects<GameObject>> variantShotWeapon;

        public PoolObjects<GameObject> poolMissiles { get; protected set; }

        public virtual void InitializationWeapon()
        {
            variantFireWeapon = GetComponent<VariantFireWeapon>();
            variantFireWeapon.InitializationWeapon(this);

            poolMissiles = new PoolObjects<GameObject>(GenerationMissile, ReturnInActive, ReturnActive, 5);
        }

        public virtual void InitializationParametrs(SpecificationFireWeapon tTXWeapon, SpecificationMissile tTXMissile)
        {
            variantFireWeapon.InitializationParametrs(tTXWeapon, tTXMissile);
        }

        public abstract void StopShooting();
        public abstract void StartShooting();

        public void SetVariantShotWeapon(Action<PoolObjects<GameObject>> action)
        {
            if (variantShotWeapon == null)
                variantShotWeapon = action;
        }

        private GameObject GenerationMissile()
        {
            var missile = Instantiate(variantFireWeapon.PrefabMissile, variantFireWeapon.ParentContainerMissile);
            missile.GetComponent<Missile>().Initialization(variantFireWeapon, this);

            return missile;
        }

        private void ReturnInActive(GameObject missile)
        {
            missile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            missile.gameObject.SetActive(false);
        }

        private void ReturnActive(GameObject missile, int position)
        {
            missile.gameObject.SetActive(true);

            if (variantFireWeapon.PointsShot.Count > position)
            {
                PositionActive(missile, position);
                missile.GetComponent<Missile>().pointShot = position;
            }
            else
            {
                PositionActive(missile, 0);
                missile.GetComponent<Missile>().pointShot = 0;

                Debug.LogException(new ArgumentException($"PointsShot (at the weapon) does not match the number of missiles fired: " +
                    $"{position + 1}/{variantFireWeapon.PointsShot.Count}"));
            }

            var spreadMissile = new Vector3(Random.Range(-variantFireWeapon.TTXWeapon.SpreadShot, variantFireWeapon.TTXWeapon.SpreadShot),
                Random.Range(-variantFireWeapon.TTXWeapon.SpreadShot, variantFireWeapon.TTXWeapon.SpreadShot), 0f);
            missile.GetComponent<Rigidbody>().velocity = (missile.transform.forward + spreadMissile) * variantFireWeapon.TTXWeapon.SpeedShot;
        }

        private void PositionActive(GameObject missile, int position)
        {
            missile.gameObject.transform.position = variantFireWeapon.PointsShot[position].position;
            missile.gameObject.transform.rotation = variantFireWeapon.PointsShot[position].rotation;
        }
    }
}