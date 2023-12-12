using UnityEngine;

public class InventorySlotForWeapon : InventorySlot
{
    [Space, SerializeField] private Transform locationItem;

    public override void Selected()
    {
        currentSprite.sprite = selectedSlot;

        //Func для LKM
        if (itemInventorying != null)
            itemInventorying.SetActionItem();
    }

    public override void Deselected()
    {
        currentSprite.sprite = deselectedSlot;
        DisableActionSlot();
    }

    protected override void PhysicalSlotForItem(Transform transform)
    {
        itemSetting.SetParant(locationItem);
        itemSetting.SetLocalPosition(Vector3.zero);
        itemSetting.SetLocalRotation(Quaternion.identity);

        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.Toolbar);
    }
}