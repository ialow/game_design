using Ddd.Application;
using Ddd.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Ddd.Domain
{
    public abstract class InventorySlot : MonoBehaviour, IKeeper
    {
        protected Image currentSprite;

        protected ISettingable itemSetting;
        protected IInventorying itemInventorying;
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

        public abstract TypeSlot TypeInventorySlot();

        public abstract void Selected();
        public abstract void Deselected();
        protected void DisableActionSlot() => PlayerController.SetActionUsingItem(null, null);

        public void TakeItem(Transform transform, Sprite sprite)
        {
            itemSetting = transform.GetComponent<ISettingable>();
            itemInventorying = transform.GetComponent<IInventorying>();

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

                itemInventorying.SetActionItem(false);
                itemInventorying.AnimationThrowItem();

                itemSetting = null;
                itemInventorying = null;
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
}