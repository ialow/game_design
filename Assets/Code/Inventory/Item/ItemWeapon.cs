using System.Collections;
using UnityEngine;

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
        yield return animationInventory.MathAnimationTake(data.AnimationTake, data.TimeCorrectionPerMeterTake);
        SetActiveCollider(true);
    }

    public override void AddItemInventorySlot()
    {
        keeper?.TakeItem(transform, data.Sprite);
    }

    public override void AnimationThrowItem()
    {
        StartCoroutine(animationInventory.MathAnimationThrow(data.TimeCorrectionPerMeterThrow));
    }
}