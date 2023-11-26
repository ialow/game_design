using UnityEngine;

public interface IKeeper
{
    public bool Full { get; set; }

    public void TakeItem(Transform transform, Sprite sprite);
    public void ThrowItem();
}