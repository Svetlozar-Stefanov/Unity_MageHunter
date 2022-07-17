using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;
    public UnityEvent OnDeathEvent;

    private void Awake()
    {
        if (OnDeathEvent == null)
        {
            OnDeathEvent = new UnityEvent();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            OnDeathEvent.Invoke();
        }
    }
}
