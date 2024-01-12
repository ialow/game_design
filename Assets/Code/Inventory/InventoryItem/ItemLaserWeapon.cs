using Ddd.Domain;
using System.Collections;

namespace Ddd.Application
{
    public class ItemLaserWeapon : Item<ItemLaserWeaponData>, IImprovable
    {
        private ushort currentLeavel = 1;

        private BaseLaserWeapons weapon;

        protected override void Awake()
        {
            base.Awake();

            weapon = GetComponent<BaseLaserWeapons>();
            weapon.Initialization(data.TTXLaserWeapon);
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
            //Debug.Log($"The functionality is not implemented - ItemLaserWeapon");

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
                //weapon.InitializationParametrs(data.ImprovementSpecificationsTTX[currentLeavel - 1].TTXLaserWeapon);
                //currentLeavel++;
            }
        }
    }
}