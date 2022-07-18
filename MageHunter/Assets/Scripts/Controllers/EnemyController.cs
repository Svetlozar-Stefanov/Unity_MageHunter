using Assets.Scripts.Controllers;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageableController<float>
{
    [SerializeField] private HealthComponent healthComponent;

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }
}
