public class ItemOther : Item<ItemOtherData>
{
    public override void AddItemInventory()
    {
        InventoryManager.Instance.TakeItemOther(transform, data.Sprite);
    }
}
