using System.Collections;
using UnityEngine;

public class PlayerAutoLooting : MonoBehaviour
{
    private PlayerParameters parameters;

    private CapsuleCollider areaLooting;

    private void Awake()
    {
        areaLooting = GetComponent<CapsuleCollider>();
        parameters = GetComponent<PlayerParameters>();

        areaLooting.radius = parameters.RadiusAutoLooting;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInventorying item))
        {
            if (item.CheckingFreeSpaceInventory())
            {
                Debug.Log("Trigger AutoLooting");
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
