using Assets.Scripts.Controllers;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageableController<float>
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EnemyInventoryComponent inventoryComponent;

    public SpriteRenderer sprite;

    private float timer = 0.0f;
    bool isHit = false;

    private void Update()
    {
        if (isHit)
        {
            timer += 0.05f;
        }

        if (timer >= 5)
        {
            sprite.color = Color.white;
            timer = 0;
            isHit = false;
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        sprite.color = Color.red;
        isHit = true;
    }

    public void OnDeathInitiated()
    {
        inventoryComponent.DropRandomLoot();
    }
}
