using Assets.Scripts.Controllers;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float damage = 10.0f;

    [SerializeField] private float lifespan = 5.0f;
    [SerializeField] private Rigidbody2D rb2d;

    void Start()
    {
        rb2d.velocity = transform.right * speed;
        //rb2d.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);

        Destroy(gameObject, lifespan);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamageableController<float> damageable = other.GetComponent<IDamageableController<float>>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
