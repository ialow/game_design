using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IDamagable
{
    [field: Header("Health parameters")]
    [field: SerializeField] public float CurrentHealth { get; protected set; } = 100f;

    public virtual void GetDamage(float damage)
    {
        var currentHealth = CurrentHealth - damage;

        if (currentHealth > 0)
            CurrentHealth = currentHealth;
        else
            OnDeath();
    }

    public virtual void OnDeath()
    {
        //throw new System.NotImplementedException();
        Destroy(gameObject);
    }

    public virtual void OnRevival()
    {
        throw new System.NotImplementedException();
    }
}
