using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour
{
    protected Image igame;
    [field: SerializeField] public bool Full { get; protected set; } = false;

    public void AddItem(Image icon)
    {
        igame = Instantiate(icon);
        Full = true;
    }

    public void RemoveItem()
    {
        Destroy(igame);

        Full = false;
    }
}
