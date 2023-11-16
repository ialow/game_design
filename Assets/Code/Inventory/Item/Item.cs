using UnityEngine;

public abstract class Item<T> : MonoBehaviour, IInventorying
{
    [SerializeField] protected T data;

    public abstract void AddItemInventory();
}