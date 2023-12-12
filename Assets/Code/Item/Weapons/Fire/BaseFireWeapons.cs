using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(WeaponSettings))]
public abstract class BaseFireWeapons : MonoBehaviour, IShootingable
{
    protected WeaponSettings weaponSettings;

    public static PoolObjects<GameObject> poolMissiles { get; protected set; }

    static BaseFireWeapons()
    {
        //poolMissiles = new PoolObjects<GameObject>(GenerationMissile, ReturnInActive, ReturnActive, 10);
    }

    protected virtual void Awake()
    {
        weaponSettings = GetComponent<WeaponSettings>();
        weaponSettings.SettingsShooting(StartShooting, StopShooting);
    }

    public abstract void StopShooting();
    public abstract void StartShooting();

    //private GameObject GenerationMissile()
    //{
    //    //Debug.Log("генерация патрона");
    //    var missile = Instantiate(_weaponVariant.TTXMissile.prefabMissile, _weaponVariant.TTXMissile.parentContainerMissile);
    //    missile.GetComponent<Missile>().Initialization(_weaponVariant, this);

    //    return missile;
    //}
}