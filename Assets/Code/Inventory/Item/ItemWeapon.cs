using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    private ItemWeaponData data;

    public override void AddItemInventory() => InventoryManager.Instance.AddItemWeapon(data.Image);
}