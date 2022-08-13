using Assets.Scripts.Controllers;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageableController<float>
{
    [Header("Components")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EnemyInventoryComponent inventoryComponent;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private AnimatorComponent animatorComponent;
 
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaypointDistance = 3.0f;

    [Header("AI Config")]
    [SerializeField] private float walkRadius = 5;
    [SerializeField] private float waitTime = 5;
    [SerializeField] private Vector2 chaseDistance = new Vector2(10, 10);

    [SerializeField] private int damage = 10;

    public SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Vector3 startPos = new Vector3(0, 0, 0);

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Vector2 targetPos;
    private float timer = 0.0f;
    private float waitTimer = 0.0f;
    private bool isHit = false;
    private bool isDead = false;

    private enum State
    {
        Waiting,
        Roaming,
        Chasing
    }
    private State state;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Destroy(gameObject);
        }
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        seeker = gameObject.GetComponent<Seeker>();
        startPos = transform.position;
        state = State.Roaming;
        seeker.StartPath(startPos, GetRandomRoamingPos(), HandlePathFound);
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
        if (path == null)
        {
            return;
        }

        if (reachedEndOfPath)
        {
            if (state == State.Roaming)
            {
                state = State.Waiting;
                animatorComponent.IsMoving(false);
            }
            else if (state == State.Waiting && waitTimer < waitTime)
            {
                waitTimer += 0.05f;
            }
            else if(waitTimer >= waitTime)
            {
                state = State.Roaming;
                waitTimer = 0;
                seeker.StartPath(rb2d.position, GetRandomRoamingPos(), HandlePathFound);
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if ((transform.position - target.transform.position).magnitude < chaseDistance.magnitude)
        {
            if (state != State.Chasing)
            {
                InvokeRepeating("UpdateTargetPath", 0.0f, 0.5f);
            }
            state = State.Chasing;
            animatorComponent.IsMoving(true);
        }
        else
        {
            if (state != State.Roaming)
            {
                seeker.StartPath(rb2d.position, GetRandomRoamingPos(), HandlePathFound);
            }
            CancelInvoke();
            state = State.Roaming;
            animatorComponent.IsMoving(true);
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        movementComponent.Move2D(direction);

        if (!reachedEndOfPath && Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void UpdateTargetPath()
    {
        seeker.StartPath(rb2d.position, target.position, HandlePathFound);
    }

    private void HandlePathFound(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
        currentWaypoint = 0;
    }

    private Vector3 GetRandomRoamingPos()
    {
        return startPos + new Vector3((Random.Range(-1, 2) * Random.Range(5.0f, walkRadius)), 0);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject == collision.gameObject)
        {
            return;
        }

        IDamageableController<float> player = collision.GetComponent<IDamageableController<float>>();
        if (player != null && collision.tag == "Player")
        {
            player.TakeDamage(damage);
            float orientation = (target.position.x - transform.position.x);
            Debug.Log(orientation);
            if (orientation < 0.0f)
            {
                rb2d.AddForce(new Vector2(10, 5), ForceMode2D.Impulse);
            }
            else if (orientation > 0.0f)
            {
                rb2d.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
            }

        }
    }
}
