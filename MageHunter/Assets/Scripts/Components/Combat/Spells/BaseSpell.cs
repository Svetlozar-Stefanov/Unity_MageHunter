using Assets.Scripts.Controllers;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpellContainer spellData;

    public SpellContainer SpellData { get => spellData; }

    private float speed;
    private float damage;
    private float cooldown;
    private bool hasLifespan;
    private float lifespan;

    public float Cooldown { get => cooldown; }

    private void Awake()
    {
        speed = spellData.speed;
        damage = spellData.damage;
        cooldown = spellData.cooldown;
        hasLifespan = spellData.hasLifespan;
        lifespan = spellData.lifespan;
    }

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
