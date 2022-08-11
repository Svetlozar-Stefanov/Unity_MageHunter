using Assets.Scripts.Controllers;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageableController<float>
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EnemyInventoryComponent inventoryComponent;
    [SerializeField] private MovementComponent movementComponent;

    [SerializeField] private float walkRadius = 5;
    [SerializeField] private Vector2 chaseDistance = new Vector2(10, 10);
    [SerializeField] private Vector2 attackRange = new Vector2(1,1);
    [SerializeField] private int damage = 10;
 
    public SpriteRenderer sprite;

    private Rigidbody2D rb2d;
    private PlayerController player;
    private Vector3 startPos = new Vector3(0,0,0);
    private float targetPos = 0;
    private bool isRoaming = true;

    private float timer = 0.0f;
    private bool isHit = false;
    private bool isDead = false;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            this.player = player;
        }
        startPos = transform.position;
        targetPos = GetRandomRoamingPos();
    }

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

    private void FixedUpdate()
    {
        if ((transform.position - player.transform.position).magnitude < attackRange.magnitude)
        {
            player.GetComponent<HealthComponent>().TakeDamage(damage);

            if (player.transform.position.x - transform.position.x > 0.0f)
            {
                rb2d.velocity = new Vector2(-20, rb2d.velocity.y);
            }
            else if(player.transform.position.x - transform.position.x < 0.0f)
            {
                rb2d.velocity = new Vector2(20, rb2d.velocity.y);
            }
            
        }
        else if ((transform.position - player.transform.position).magnitude < chaseDistance.magnitude)
        {
            isRoaming = false;
            targetPos = player.transform.position.x;
        }
        else
        {
            isRoaming = true;
        }
        HandleMovement();
    }

    //private float GetDistance(Vector2 v1, Vector2 v2)
    //{
    //    return Mathf.Sqrt(((v1.x - v2.x) * (v1.x - v2.x)) + ((v1.y - v2.y) * (v1.y - v2.y)));
    //}

    private void HandleMovement()
    {
        float distanceBetween = (transform.position.x - targetPos);

        if (Mathf.Abs(distanceBetween) < 0.1f && isRoaming)
        {
            targetPos = GetRandomRoamingPos();
        }
        else if (distanceBetween < 0.0f)
        {
            movementComponent.Move(1);
        }
        else if (distanceBetween > 0.0f)
        {
            movementComponent.Move(-1);
        }
    }

    private float GetRandomRoamingPos()
    {
        return startPos.x + Random.Range(-1, 2) * Random.Range(1.0f, walkRadius);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        sprite.color = Color.red;
        isHit = true;
    }

    public void OnDeathInitiated()
    {
        if (!isDead)
        {
            isDead = true;
            inventoryComponent.DropRandomLoot();
            Destroy(gameObject);
        }
    }
}
