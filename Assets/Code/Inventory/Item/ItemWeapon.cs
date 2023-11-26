public class ItemWeapon : Item<ItemWeaponData>
{
    public override void AddItemInventory()
    {
        InventoryManager.Instance.TakeItemWeapon(transform, data.Sprite);
    }
}