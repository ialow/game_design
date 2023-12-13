using System.Collections;

public interface IInventorying
{
    public bool CheckingFreeSpaceInventory();
    public IEnumerator AnimationTakeItem();
    public void AddItemInventorySlot();
    public void AnimationThrowItem();

    public void SetActionItem();
}