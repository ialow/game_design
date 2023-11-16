using UnityEngine;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IKeeper
{
    [Header("Only UI")]
    [SerializeField] protected GameObject visualPrafabItem;

    [Header("Only settings")]
    [SerializeField] protected Transform locationInactiveItem;

    protected Transform item;
    protected GameObject visualItem;

    [field: SerializeField] public bool Full { get; set; } = false;

    public void AddItemSlot(Transform transform, Sprite sprite)
    {
        AddVisualItemSlot(sprite);
        AddPhysicalItemSlot(transform);
        Full = true;

        Debug.Log("Add item inventory");
    }

    protected virtual void AddVisualItemSlot(Sprite sprite)
    {
        visualItem = Instantiate(visualPrafabItem, transform);
        visualItem.GetComponent<Image>().sprite = sprite;
    }

    protected virtual void AddPhysicalItemSlot(Transform transform)
    {
        item = transform;
        item.SetParent(locationInactiveItem);
        item.gameObject.SetActive(false);
    }
}