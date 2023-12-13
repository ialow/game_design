using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLaserWeapon : Item<ItemLaserWeaponData>
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

    public override void SetActionItem()
    {
        Debug.Log($"The functionality is not implemented - ItemWeapon");
        //PlayerController.SetActionUsingItem();
    }
}
