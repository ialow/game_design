using Ddd.Domain;
using UnityEngine;

namespace Ddd.Application
{
    public class DefaultLaser : BaseLaserWeapons
    {
        [Header("Visual effects")]
        [SerializeField] private ParticleSystem muzzle;
        [SerializeField] private ParticleSystem hitPoint;

        protected override void ActivateLaserVFX()
        {
            base.ActivateLaserVFX();
            muzzle.Play();
            hitPoint.Play();
        }

        protected override void DeactivateLaserVFX()
        {
            base.DeactivateLaserVFX();
            muzzle.Stop();
            hitPoint.Stop();
        }

        protected override void SetParametersVFX()
        {
            base.SetParametersVFX();
            hitPoint.transform.position = hitPosition;
        }
    }
}