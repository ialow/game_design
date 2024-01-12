using UnityEngine;

namespace Ddd.Domain
{
    public interface IKeeper
    {
        public TypeSlot TypeInventorySlot();
        public bool Full { get; set; }

        public void TakeItem(Transform transform, Sprite sprite);
        public void ThrowItem();
    }
}