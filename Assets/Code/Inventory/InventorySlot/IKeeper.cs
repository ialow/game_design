using UnityEngine;

public interface IKeeper
{
    public bool Full { get; set; }

    public void AddItemSlot(Transform transform, Sprite sprite);
}