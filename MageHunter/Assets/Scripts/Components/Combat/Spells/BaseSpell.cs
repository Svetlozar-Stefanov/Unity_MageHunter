using Assets.Scripts.Controllers;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] public float cooldown = 10;

    [SerializeField] private bool hasLifespan = true;
    [SerializeField] private float lifespan = 5.0f;

    [SerializeField] private Rigidbody2D rb2d;

    public virtual void Use()
    {
        if (hasLifespan)
        {
            Destroy(gameObject, lifespan);
        }

        rb2d.velocity = transform.right * speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageableController<float> damageable = other.GetComponent<IDamageableController<float>>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
