using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float health;

    [Header("Events")]
    [SerializeField] private UnityEvent onTakeDamage;
    [SerializeField] private UnityEvent onDeathEvent;

    private void Awake()
    {
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

    protected virtual void Die()
    {
        onDeathEvent.Invoke();
        Destroy(gameObject);
    }
}
