using System.Collections;
using UnityEngine;

public abstract class Item<T> : MonoBehaviour, IInventorying, ISettingable
{
    protected Collider colider;

    [SerializeField] protected T data;

    private void Awake()
    {
        colider = GetComponent<Collider>();
    }

    public abstract void AddItemInventory();

    public void SetParant(Transform parant) => gameObject.transform.parent = parant;
    public void SetActive(bool activeted) => gameObject.SetActive(activeted);
    public void SetLocalPosition(Vector3 position) => transform.localPosition = position;
    public void SetLocalRotation(Quaternion rotation) => transform.localRotation = rotation;
    public void SetInvisibleColiderForSeconds(float second) => StartCoroutine(SetInvisibleColiderCoroutine(second));

    private IEnumerator SetInvisibleColiderCoroutine(float second)
    {
        colider.enabled = false;
        yield return new WaitForSeconds(second);
        colider.enabled = true;
    }
}