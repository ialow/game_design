using Ddd.Infrastructure;
using System.Collections;
using UnityEngine;

namespace Ddd.Application
{
    public class PlayerAutoLooting : MonoBehaviour
    {
        private Player parameters;

        [SerializeField] private CapsuleCollider areaLooting;

        private void Awake()
        {
            parameters = GetComponent<Player>();

            InitializationParameters();
        }

        public void InitializationParameters()
        {
            areaLooting.radius = parameters.RadiusAutoLooting;

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IInventorying item))
            {
                if (item.CheckingFreeSpaceInventory())
                {
                    Debug.Log("Trigger AutoLooting"); // Требуется корректировка;
                    StartCoroutine(TakeItem(item));
                }
            }
        }

        private IEnumerator TakeItem(IInventorying item)
        {
            yield return item.AnimationTakeItem();
            item.AddItemInventorySlot();
        }
    }
}