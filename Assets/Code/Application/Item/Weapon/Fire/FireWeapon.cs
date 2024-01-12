using Ddd.Domain;
using System.Collections;
using UnityEngine;

namespace Ddd.Application
{
    public class FireWeapon : BaseFireWeapons
    {
        private Coroutine shooting;
        private float timeStartShotting;

        private bool canShot = true;

        public override void StartShooting()
        {
            shooting = StartCoroutine(Shooting());
        }

        public override void StopShooting()
        {
            if (shooting != null) StopCoroutine(shooting);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                Shot();

                var remainingTimeRecharge = Time.time - timeStartShotting;
                yield return remainingTimeRecharge;
            }
        }

        private void Shot()
        {
            if (canShot)
            {
                variantShotWeapon(poolMissiles);

                canShot = false;
                StartCoroutine(Refresh());
            }
        }

        private IEnumerator Refresh()
        {
            timeStartShotting = Time.time;
            yield return new WaitForSeconds(variantFireWeapon.TTXWeapon.CooldownTime);
            canShot = true;
        }
    }
}