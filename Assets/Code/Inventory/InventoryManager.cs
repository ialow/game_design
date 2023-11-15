using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Slots UI")]
    [SerializeField] private List<InventorySlotForOther> inventorySlotForOther;
    [SerializeField] private List<InventorySlotForWeapon> inventorySlotForWeapon;

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // refactoring
    public void AddItemOther(Image imageOther)
    {
        for (var i = 0; i < inventorySlotForOther.Count; i++)
        {
            var slot = inventorySlotForOther[i].GetComponent<InventorySlotForOther>();

            if (!slot.Full)
            {
                slot.AddItem(imageOther);
                return;
            }
        }
    }

    public void AddItemWeapon(Image imageWeapon)
    {
        for (var i = 0; i < inventorySlotForWeapon.Count; i++)
        {
            var slot = inventorySlotForOther[i].GetComponent<InventorySlotForWeapon>();

            if (!slot.Full)
            {
                slot.AddItem(imageWeapon);
                return;
            }
        }
    }
    // 
}