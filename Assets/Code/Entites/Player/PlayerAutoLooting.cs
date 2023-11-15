using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoLooting : MonoBehaviour
{
    private PlayerParameters parameters;

    [SerializeField] private CapsuleCollider areaLooting;

    private void Awake()
    {
        areaLooting.radius = parameters.DistanceAutoLooting;
    }

    // Заменить на stay (проверить)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Item item)) 
            item.AddItemInventory();
    }
}
