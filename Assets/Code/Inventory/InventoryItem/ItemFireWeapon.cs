using Ddd.Domain;
using System.Collections;
using UnityEngine;

namespace Ddd.Application
{
    public class ItemFireWeapon : Item<ItemFireWeaponData>, IImprovable
    {
        private ushort currentLeavel = 1;

        private BaseFireWeapons weapon;

        protected override void Awake()
        {
            base.Awake();

            weapon = GetComponent<BaseFireWeapons>();
            weapon.InitializationWeapon();
            weapon.InitializationParametrs(data.TTXFireWeapon, data.TTXFireMissile);
        }

        public int CurrentLeavel => currentLeavel;
        public int MaxLeavel => data.MaxLeavel;

        public override bool CheckingFreeSpaceInventory()
        {
            keeper = InventoryManager.Instance.CheckingFreeSpaceItemWeapon(data.InventorySlot);
            return keeper != null ? true : false;
        }

        public override IEnumerator AnimationTakeItem()
        {
            SetActiveCollider(false);
            yield return animationInventory.MathAnimationTake(data.AnimationTake, data.TimeCorrectionPerMeterTake);
            SetActiveCollider(true);
        }

        public override void AddItemInventorySlot()
        {
            keeper?.TakeItem(transform, data.Sprite);
        }

        public override void AnimationThrowItem()
        {
            StartCoroutine(animationInventory.MathAnimationThrow(data.TimeCorrectionPerMeterThrow));
        }

        public override void SetActionItem(bool enable = true)
        {
            if (enable)
            {
                PlayerController.SetActionUsingItem(weapon.StartShooting, weapon.StopShooting);
            }
            else
            {
                weapon.StopShooting();
                PlayerController.SetActionUsingItem(null, null);
            }
        }

        //[ContextMenu("UpLevels")]
        public void UpLevels()
        {
            if (currentLeavel < MaxLeavel)
            {
                //weapon.InitializationParametrs(data.ImprovementSpecificationsTTX[currentLeavel - 1].TTXWeapon, 
                //    data.ImprovementSpecificationsTTX[currentLeavel - 1].TTXMissile);
                //currentLeavel++;
            }
        }
    }
}