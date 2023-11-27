using System.Collections;

public class ItemWeapon : Item<ItemWeaponData>
{
    public override bool CheckingFreeSpaceInventory()
    {
        keeper = InventoryManager.Instance.CheckingFreeSpaceItemWeapon();
        return keeper != null ? true : false;
    }

    public override IEnumerator AnimationTakeItem()
    {
        SetActiveCollider(false);
        yield return animationInventory.MathAnimationTake(data.AnimationTake, data.TimeAnimationTake);
        SetActiveCollider(true);
    }

    public override void AddItemInventorySlot()
    {
        keeper?.TakeItem(transform, data.Sprite);
    }
}