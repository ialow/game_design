using System.Collections.Generic;
using UnityEngine;

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

    public void AddItemOther(Transform transform, Sprite sprite)
    {
        AddItem(inventorySlotForOther, transform, sprite);
    }

    public void AddItemWeapon(Transform transform, Sprite sprite)
    {
        AddItem(inventorySlotForWeapon, transform, sprite);
    }

    public void AddItem<T>(List<T> inventorySlots, Transform transform, Sprite sprite)
        where T : Component
    {
        for (var i = 0; i < inventorySlots.Count; i++)
        {
            var slot = inventorySlots[i].GetComponent<IKeeper>();

            if (!slot.Full)
            {
                slot.AddItemSlot(transform, sprite);
                return;
            }
        }
    }
}