using System.Collections;

public interface IInventorying
{
    public bool CheckingFreeSpaceInventory();
    public IEnumerator AnimationTakeItem();
    public void AddItemInventorySlot();
}