using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;

    void Start()
    {
        rb2d.velocity = transform.right * speed;
        //rb2d.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);    
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.healthComponent.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
