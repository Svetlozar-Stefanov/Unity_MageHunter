using Assets.Scripts.Controllers;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private SpellScroll spell;
    [SerializeField] private Rigidbody2D rb2d;

    public SpellScroll Data { get => spell; }

    public Spell(SpellScroll data)
    {
        spell = data;
    }

    private void Awake()
    {
        GameObject prefabGameObj = Instantiate(spell.prefab);
        //gameObject.transform.localScale = prefabGameObj.transform.localScale;
        prefabGameObj.transform.SetParent(this.transform);
        prefabGameObj.transform.localPosition = Vector3.zero;
        prefabGameObj.transform.rotation = Quaternion.identity;

        //TO ADD WHEN ADDING NON-TEST SPELLS
        //CircleCollider2D collider2D = this.gameObject.AddComponent<CircleCollider2D>();
        //CircleCollider2D prefabCollider2D = prefabGameObj.GetComponent<CircleCollider2D>();

        //collider2D.isTrigger = true;

        //collider2D.offset = prefabCollider2D.offset;
        //collider2D.radius = prefabCollider2D.radius;
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
