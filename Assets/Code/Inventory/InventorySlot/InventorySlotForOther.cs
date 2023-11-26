using UnityEngine;

public class InventorySlotForOther : InventorySlot
{
    [Space, SerializeField] private Transform locationInactiveItem;
    [SerializeField] private Transform locationActiveItem;

    public override void Selected()
    {
        currentSprite.sprite = selectedSlot;

        if (Full)
        {
            item.SetParant(locationActiveItem);
            item.SetLocalPosition(Vector3.zero);
            item.SetLocalRotation(Quaternion.identity);
            item.SetActive(true);
            //Func для LKM
        }
    }

    public override void Deselected()
    {
        currentSprite.sprite = deselectedSlot;

        if (Full) 
        {
            item.SetParant(locationInactiveItem);
            item.SetActive(false);
        }
    }

    protected override void PhysicalSlotForItem(Transform transform)
    {
        item = transform.GetComponent<ISettingable>();

        item.SetParant(locationInactiveItem);
        item.SetActive(false);

        InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.InventorySlot);
    }
}