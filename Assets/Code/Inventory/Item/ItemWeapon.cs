public class ItemWeapon : Item<ItemWeaponData>
{
    public override void AddItemInventory()
    {
        InventoryManager.Instance.AddItemWeapon(transform, data.Sprite);
    }
}