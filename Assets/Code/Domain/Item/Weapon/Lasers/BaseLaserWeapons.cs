using Ddd.Infrastructure;
using System.Collections;
using UnityEngine;

namespace Ddd.Domain
{
    public abstract class BaseLaserWeapons : MonoBehaviour, IShootingable
    {
        protected Coroutine shooting;
        protected SpecificationLasersWeapon tTXWeapon;

        [Header("Base settings")]
        [SerializeField] protected Transform muzzlePoint;

        [SerializeField] protected LineRenderer beam;
        [SerializeField] protected LayerMask layerMask;

        protected bool hitCast;
        protected RaycastHit hitInfo;
        protected Vector3 hitPosition;

        protected virtual void ActivateLaserVFX() => beam.enabled = true;
        protected virtual void DeactivateLaserVFX() => beam.enabled = false;

        public void Initialization(SpecificationLasersWeapon tTXWeapon)
        {
            this.tTXWeapon = tTXWeapon;
        }

        protected void HitInfo()
        {
            var ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
            hitCast = Physics.Raycast(ray, out RaycastHit hitInfo, tTXWeapon.MaxDistance, layerMask);
            hitPosition = hitCast ? hitInfo.point : muzzlePoint.position + muzzlePoint.forward * tTXWeapon.MaxDistance;
            this.hitInfo = hitInfo;
        }

        protected virtual void SetParametersVFX()
        {
            beam.SetPosition(0, muzzlePoint.transform.position);
            beam.SetPosition(1, hitPosition);
        }

        public virtual void StartShooting()
        {
            ActivateLaserVFX();
            shooting = StartCoroutine(Shooting());
        }

        public virtual void StopShooting()
        {
            if (shooting != null) StopCoroutine(shooting);
            DeactivateLaserVFX();
        }

        protected virtual IEnumerator Shooting()
        {
            var damage = tTXWeapon.DamagePreSecond * Time.fixedDeltaTime;

            while (beam.enabled)
            {
                HitInfo();
                SetParametersVFX();

                if (hitCast && hitInfo.transform.root.TryGetComponent(out IDamagable entity))
                {
                    //Debug.Log("The damage done " + damage);
                    entity.GetDamage(damage);
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}