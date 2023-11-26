using UnityEngine;

public class InventorySlotForWeapon : InventorySlot
{
    [Space, SerializeField] private Transform locationItem;

    public override void Selected()
    {
        currentSprite.sprite = selectedSlot;
        //Func для LKM
    }

    public override void Deselected()
    {
        currentSprite.sprite = deselectedSlot;
    }

    protected override void PhysicalSlotForItem(Transform transform)
    {
        item = transform.GetComponent<ISettingable>();

        item.SetParant(locationItem);
        item.SetLocalPosition(Vector3.zero);
        item.SetLocalRotation(Quaternion.identity);

        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.InventorySlot);
    }
}