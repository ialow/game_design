using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSettings : MonoBehaviour, IShootingable
{
    private Action EnableShooting;
    private Action DisenableShooting;

    public void StartShooting()
    {
        EnableShooting();
    }

    public void StopShooting()
    {
        DisenableShooting();
    }

    public void SettingsShooting(Action startShooting, Action stopShooting)
    {
        EnableShooting = startShooting;
        DisenableShooting = stopShooting;
    }
}