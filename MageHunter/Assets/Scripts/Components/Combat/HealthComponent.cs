using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float MaxHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent onTakeDamage;
    [SerializeField] private UnityEvent onDeathEvent;

    private float health;

    private void Awake()
    {
        health = MaxHealth;

        if (onDeathEvent == null)
        {
            onDeathEvent = new UnityEvent();
        }
        if (onTakeDamage == null)
        {
            onTakeDamage = new UnityEvent();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        onTakeDamage.Invoke();

        if (health <= 0.0f)
        {
            Die();
        }
    }

    protected void Die()
    {
        onDeathEvent.Invoke();
        Destroy(gameObject);
    }
}
