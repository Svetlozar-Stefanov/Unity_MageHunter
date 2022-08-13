using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float maxHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent onTakeDamage;
    [SerializeField] private UnityEvent onDeathEvent;

    private float health;

    public float Health { get => health; }

    public float MaxHealth { get => maxHealth; }

private void Awake()
    {
        health = maxHealth;

        if (onDeathEvent == null)
        {
            onDeathEvent = new UnityEvent();
        }
        if (onTakeDamage == null)
        {
            onTakeDamage = new UnityEvent();
        }
    }

    public virtual void Heal(float amount)
    {
        if (amount < 0.0f)
        {
            return;
        }

        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
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
    }
}
