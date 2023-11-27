using UnityEngine;

public class InventorySlotForOther : InventorySlot
{
    [Space, SerializeField] private Transform locationInactiveItem;
    [SerializeField] private Transform locationActiveItem;

    public override void Selected()
    {
        currentSprite.sprite = selectedSlot;

        if (itemSetting != null)
        {
            itemSetting.SetParant(locationActiveItem);
            itemSetting.SetLocalPosition(Vector3.zero);
            itemSetting.SetLocalRotation(Quaternion.identity);
            itemSetting.SetActive(true);
            //Func ��� LKM
        }
    }

    public override void Deselected()
    {
        currentSprite.sprite = deselectedSlot;

        if (itemSetting != null) 
        {
            itemSetting.SetParant(locationInactiveItem);
            itemSetting.SetActive(false);
        }
    }

    protected override void PhysicalSlotForItem(Transform transform)
    {
        itemSetting.SetParant(locationInactiveItem);
        itemSetting.SetActive(false);

        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.Toolbar);
    }
}