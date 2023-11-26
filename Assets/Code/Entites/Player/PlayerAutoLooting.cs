using UnityEngine;

public class PlayerAutoLooting : MonoBehaviour
{
    private PlayerParameters parameters;

    private CapsuleCollider areaLooting;

    private void Awake()
    {
        areaLooting = GetComponent<CapsuleCollider>();
        parameters = GetComponent<PlayerParameters>();

        areaLooting.radius = parameters.DistanceAutoLooting;
    }

    // «аменить на stay (проверить на массе + цикличность)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInventorying item))
        {
            Debug.Log("Trigger AutoLooting");
            item.AddItemInventory();
        }
    }
}
