using Ddd.Infrastructure;
using System.Collections;
using UnityEngine;

namespace Ddd.Domain
{
    public abstract class Item<T> : MonoBehaviour, IInventorying, ISettingable
    {
        protected IKeeper keeper;

        protected Collider colider;
        protected AnimationItemForInventory animationInventory;

        [SerializeField] protected T data;

        protected virtual void Awake()
        {
            colider = GetComponent<Collider>();
            animationInventory = GetComponent<AnimationItemForInventory>();
        }

        public abstract void AddItemInventorySlot();
        public abstract IEnumerator AnimationTakeItem();
        public abstract bool CheckingFreeSpaceInventory();
        public abstract void AnimationThrowItem();
        public abstract void SetActionItem(bool enable = true);

        public void SetParant(Transform parant) => gameObject.transform.parent = parant;
        public void SetActive(bool activeted) => gameObject.SetActive(activeted);
        public void SetActiveCollider(bool activeted) => colider.enabled = activeted;
        public void SetLocalPosition(Vector3 position) => transform.localPosition = position;
        public void SetLocalRotation(Quaternion rotation) => transform.localRotation = rotation;
        public void SetInvisibleColiderForSeconds(float second) => StartCoroutine(SetInvisibleColiderCoroutine(second));
        public void BreakDependency() => keeper = null;

        private IEnumerator SetInvisibleColiderCoroutine(float second)
        {
            SetActiveCollider(false);
            yield return new WaitForSeconds(second);
            SetActiveCollider(true);
        }
    }
}