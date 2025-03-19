using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    public float chaseSpeed = 4f;       // Speed when chasing the player
    public float floatSpeed = 2f;       // Speed while floating
    public float floatRadius = 3f;      // Distance boss can float randomly
    public float floatPauseTime = 1.5f; // Pause before floating again
    public float detectionRange = 5f;   // Distance at which the boss detects the player
    public float damageInterval = 1f;   // Time between damage instances
    public int damageAmount = 10;       // Damage the player takes per interval

    private Transform player;
    private Vector2 floatTarget;
    private bool isChasing = false;
    private Coroutine floatingRoutine;
    private SpriteRenderer spriteRenderer;
    private Coroutine damageRoutine; // Keeps track of the damage coroutine

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        floatingRoutine = StartCoroutine(FloatAround()); // Start floating movement
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isChasing)
            {
                Debug.Log("Player detected! Boss is now chasing.");
                isChasing = true;

                if (floatingRoutine != null)
                {
                    StopCoroutine(floatingRoutine);
                    floatingRoutine = null;
                }
            }
        }
        else
        {
            if (isChasing)
            {
                Debug.Log("Player left detection range! Boss returns to floating.");
                isChasing = false;
                floatingRoutine = StartCoroutine(FloatAround());
            }
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    IEnumerator FloatAround()
    {
        while (!isChasing) // Keep floating when not chasing
        {
            floatTarget = GetRandomFloatPosition();
            Debug.Log("Floating to: " + floatTarget);

            yield return MoveToPosition(floatTarget, floatSpeed);

            Debug.Log("Pausing at float position...");
            yield return new WaitForSeconds(floatPauseTime);
        }
    }

    Vector2 GetRandomFloatPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * floatRadius;
        return (Vector2)transform.position + randomOffset;
    }

    IEnumerator MoveToPosition(Vector2 target, float speed)
    {
        while (Vector2.Distance(transform.position, target) > 0.2f && !isChasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Flip sprite based on direction (default faces left)
            spriteRenderer.flipX = target.x > transform.position.x;

            yield return null;
        }
    }

    void ChasePlayer()
    {
        Debug.Log("Boss is chasing the player!");
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Flip sprite based on direction (default faces left)
        spriteRenderer.flipX = player.position.x > transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Boss is on top of the player!");
            if (damageRoutine == null)
            {
                damageRoutine = StartCoroutine(DealDamageOverTime(other.GetComponent<PlayerHealth>()));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Boss is no longer touching the player.");
            if (damageRoutine != null)
            {
                StopCoroutine(damageRoutine);
                damageRoutine = null;
            }
        }
    }

    IEnumerator DealDamageOverTime(PlayerHealth playerHealth)
    {
        while (playerHealth != null) // Ensure the player exists
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Player took " + damageAmount + " damage!");

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
