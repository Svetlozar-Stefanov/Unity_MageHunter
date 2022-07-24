using Assets.Scripts.Controllers;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpellContainer spellData;

    public SpellContainer SpellData { get => spellData; }

    public float Speed { get => spellData.speed; }
    public float Damage { get => spellData.damage; }
    public float Cooldown { get => spellData.cooldown; }
    public bool HasLifespan { get => spellData.hasLifespan; }
    public float Lifespan { get => spellData.lifespan; }
    public float ManaCost { get => spellData.manaCost; }

    public virtual void Use()
    {
        if (HasLifespan)
        {
            Destroy(gameObject, Lifespan);
        }

        rb2d.velocity = transform.right * Speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageableController<float> damageable = other.GetComponent<IDamageableController<float>>();
        if (damageable != null)
        {
            damageable.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
