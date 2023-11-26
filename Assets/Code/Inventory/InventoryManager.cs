using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int countSlotForOther;
    private int countSlotForWeapon;
    private InventorySlot selectedSlot = null;

    [Header("Slots list UI")]
    [SerializeField] private List<InventorySlotForOther> inventorySlotForOther;
    [SerializeField] private List<InventorySlotForWeapon> inventorySlotForWeapon;

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        countSlotForOther = inventorySlotForOther.Count;
        countSlotForWeapon = inventorySlotForWeapon.Count;
    }

    public void ChangeSelectedSlot(int newSlot)
    {
        selectedSlot?.Deselected();

        if (0 < newSlot && newSlot <= countSlotForOther)
            selectedSlot = inventorySlotForOther[newSlot - 1];
        if (countSlotForOther < newSlot && newSlot <= countSlotForOther + countSlotForWeapon)
            selectedSlot = inventorySlotForWeapon[newSlot - countSlotForOther - 1];

        selectedSlot?.Selected();
    }

    public void TakeItemOther(Transform transform, Sprite sprite)
    {
        TakeItem(inventorySlotForOther, transform, sprite);
    }

    public void TakeItemWeapon(Transform transform, Sprite sprite)
    {
        TakeItem(inventorySlotForWeapon, transform, sprite);
    }

    public void TakeItem<T>(List<T> inventorySlots, Transform transform, Sprite sprite)
        where T : Component
    {
        for (var i = 0; i < inventorySlots.Count; i++)
        {
            var slot = inventorySlots[i].GetComponent<IKeeper>();

            if (!slot.Full)
            {
                slot.TakeItem(transform, sprite);
                return;
            }
        }
    }

    public void ThrowItem()
    {
        selectedSlot?.ThrowItem();
    }
}