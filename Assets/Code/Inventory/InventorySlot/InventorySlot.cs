using UnityEngine;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IKeeper
{
    protected Image currentSprite;

    protected ISettingable itemSetting;
    protected GameObject visualItem;

    [Header("Only UI")]
    [SerializeField] protected Sprite selectedSlot;
    [SerializeField] protected Sprite deselectedSlot;

    [Space, SerializeField] protected GameObject visualSlotForItem;

    [Header("Only settings")]
    [SerializeField] protected float timeIgnoringItem;
    [field: SerializeField] public bool Full { get; set; } = false;

    private void Awake()
    {
        currentSprite = transform.gameObject.GetComponent<Image>();
        Deselected();
    }

    public abstract void Selected();
    public abstract void Deselected();

    public void TakeItem(Transform transform, Sprite sprite)
    {
        itemSetting = transform.GetComponent<ISettingable>();

        VisualSlotForItem(sprite);
        PhysicalSlotForItem(transform);
        Debug.Log("Add item inventory");
    }

    public void ThrowItem()
    {
        if (itemSetting != null)
        {
            Full = false;
            VisualSlotForItem(null);

            itemSetting.SetInvisibleColiderForSeconds(timeIgnoringItem);
            itemSetting.BreakDependency();
            itemSetting.SetParant(null);
            itemSetting = null;
            Debug.Log("Throw item");
        }
    }

    protected virtual void VisualSlotForItem(Sprite sprite)
    {
        if (sprite != null)
        {
            visualSlotForItem.GetComponent<Image>().sprite = sprite;
            visualSlotForItem.SetActive(true);
        }
        else
        {
            visualSlotForItem.SetActive(false);
            visualSlotForItem.GetComponent<Image>().sprite = sprite;
        }
    }

    protected abstract void PhysicalSlotForItem(Transform transform);
}