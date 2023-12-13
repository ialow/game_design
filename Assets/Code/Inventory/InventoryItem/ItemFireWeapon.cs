using System.Collections;
using UnityEngine;

public class ItemFireWeapon : Item<ItemFireWeaponData>
{
    private BaseFireWeapons weapon;

    protected override void Awake()
    {
        base.Awake();

        weapon = GetComponent<BaseFireWeapons>();
        weapon.InitializationWeapon();
        weapon.InitializationParametrs(data.TTXWeapon, data.TTXMissile);
    }

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
        PlayerController.SetActionUsingItem(weapon.StartShooting, weapon.StopShooting);
    }
}