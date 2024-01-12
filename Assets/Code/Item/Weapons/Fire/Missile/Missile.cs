using Ddd.Infrastructure;
using UnityEngine;

namespace Ddd.Domain
{
    public class Missile : MonoBehaviour
    {
        [HideInInspector] public int pointShot = 0;

        private const int idLayer = 27;

        private Rigidbody rb;
        private VariantFireWeapon weaponVariant;
        private BaseFireWeapons weapon;

        private float distance;

        private void Awake()
        {
            gameObject.layer = idLayer;

            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        private void Update()
        {
            distance = Vector3.Distance(weaponVariant.PointsShot[pointShot].position, transform.position);

            if (distance > weaponVariant.TTXMissile.MaxDistance)
                weapon.poolMissiles.ReturnInActive(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.root.TryGetComponent(out IDamagable entity))
            {
                var damage = DamageCalculation(collision.transform.position);
                Debug.Log("The damage done " + damage);
                entity.GetDamage(damage);
            }
            weapon.poolMissiles.ReturnInActive(gameObject);
        }

        private float DamageCalculation(Vector3 pointHit)
        {
            distance = Vector3.Distance(weaponVariant.PointsShot[pointShot].position, pointHit);

            if (distance < weaponVariant.TTXMissile.DistanceWithMaxDamage)
            {
                return weaponVariant.TTXMissile.Damage;
            }
            else
            {
                var exceedingDistance = Mathf.Round(distance - weaponVariant.TTXMissile.DistanceWithMaxDamage);
                var damage = weaponVariant.TTXMissile.Damage * Mathf.Pow((1 - weaponVariant.TTXMissile.DamageDropCoefficient / 1), exceedingDistance);
                return damage;
            }
        }

        public void UpdatinParameters(VariantFireWeapon weaponVariant) => this.weaponVariant = weaponVariant;

        public void Initialization(VariantFireWeapon weaponVariant, BaseFireWeapons weapon)
        {
            this.weaponVariant = weaponVariant;
            this.weapon = weapon;
        }
    }
}