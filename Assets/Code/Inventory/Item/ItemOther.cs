using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOther : Item
{
    private ItemOtherData data;

    public override void AddItemInventory() => InventoryManager.Instance.AddItemWeapon(data.Image);
}
