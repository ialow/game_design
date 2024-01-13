using Ddd.Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ddd.Application
{
    public class InventoryManager : MonoBehaviour
    {
        private IKeeper slot;
        private int countSlotForOther;
        private int countSlotForWeapon;
        private InventorySlot selectedSlot = null;

        [Header("Slots list UI")]
        [SerializeField] private List<InventorySlotForOther> inventorySlotForOther;
        [SerializeField] private List<InventorySlotForWeapon> inventorySlotForWeapon;

        public static InventoryManager Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
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

        public IKeeper CheckingFreeSpaceItemOther()
        {
            return CheckingFreeSpace(inventorySlotForOther, new List<TypeSlot> { TypeSlot.OtherItem }, CheckingTypeItemAndSlot);
        }

        public IKeeper CheckingFreeSpaceItemWeapon(List<TypeSlot> itemType)
        {
            return CheckingFreeSpace(inventorySlotForWeapon, itemType, CheckingTypeItemAndSlot);
        }

        private bool CheckingTypeItemAndSlot(List<TypeSlot> item) => item.Contains(slot.TypeInventorySlot());

        public IKeeper CheckingFreeSpace<T>(List<T> inventorySlots, List<TypeSlot> itemType, Func<List<TypeSlot>, bool> CheckingTypeItemAndSlot)
            where T : Component
        {
            for (var i = 0; i < inventorySlots.Count; i++)
            {
                slot = inventorySlots[i].GetComponent<IKeeper>();

                if (!slot.Full && CheckingTypeItemAndSlot(itemType))
                {
                    slot.Full = true;
                    return slot;
                }
            }
            return null;
        }

        public void ThrowItem()
        {
            selectedSlot?.ThrowItem();
        }
    }
}