public class ItemOther : Item<ItemOtherData>
{
    public override void AddItemInventory()
    {
        InventoryManager.Instance.AddItemOther(transform, data.Sprite);
    }
}
