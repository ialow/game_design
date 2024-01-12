using Ddd.Domain;
using UnityEngine;

namespace Ddd.Application
{
    public class InventorySlotForWeapon : InventorySlot
    {
        [SerializeField] private TypeSlot typeSlot;

        [Space, SerializeField] private Transform locationItem;

        public override TypeSlot TypeInventorySlot() => typeSlot;

        public override void Selected()
        {
            currentSprite.sprite = selectedSlot;

            //Func для LKM
            if (itemInventorying != null)
                itemInventorying.SetActionItem();
        }

        public override void Deselected()
        {
            currentSprite.sprite = deselectedSlot;

            if (itemInventorying != null)
                itemInventorying.SetActionItem(false);
            else
                DisableActionSlot();
        }

        protected override void PhysicalSlotForItem(Transform transform)
        {
            itemSetting.SetParant(locationItem);
            itemSetting.SetLocalPosition(Vector3.zero);
            itemSetting.SetLocalRotation(Quaternion.identity);

            InventoryManager.Instance.ChangeSelectedSlot(InputParametrs.Toolbar);
        }
    }
}