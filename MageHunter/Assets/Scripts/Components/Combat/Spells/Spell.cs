using Assets.Scripts.Controllers;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private SpellScroll spell;
    [SerializeField] private Rigidbody2D rb2d;

    public SpellScroll Data { get => spell; set => spell = value; }

    private void Awake()
    {
        GameObject visuals = Instantiate(spell.prefab);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;

    }

    public void Cast()
    {
        if (spell.HasLifespan)
        {
            Destroy(gameObject, spell.Lifespan);
        }

        rb2d.velocity = transform.right * spell.Speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamageableController<float> damageable = other.GetComponent<IDamageableController<float>>();
        if (damageable != null)
        {
            damageable.TakeDamage(spell.Damage);
            Destroy(gameObject);
        }
    }
}
