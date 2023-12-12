using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapons : BaseFireWeapons
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
            //VariantShotWeapon(poolMissiles);

            canShot = false;
            StartCoroutine(Refresh());
        }
    }

    private IEnumerator Refresh()
    {
        timeStartShotting = Time.time;
        yield return new WaitForSeconds(/*weaponVariant.TTXWeapon.CooldownTime*/3f);
        canShot = true;
    }
}