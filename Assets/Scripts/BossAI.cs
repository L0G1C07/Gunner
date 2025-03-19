using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    public float chaseSpeed = 4f;
    public float floatSpeed = 2f;
    public float floatRadius = 3f;
    public float floatPauseTime = 1.5f;
    public float detectionRange = 5f;
    public float damageInterval = 1f;
    public int damageAmount = 10;

    private Transform player;
    private Vector2 floatTarget;
    private bool isChasing = false;
    private Coroutine floatingRoutine;
    private Coroutine damageRoutine;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

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
        while (!isChasing)
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
            rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime));

            // Flip sprite based on direction
            spriteRenderer.flipX = target.x > rb.position.x;

            yield return null;
        }
    }

    void ChasePlayer()
    {
        Debug.Log("Boss is chasing the player!");

        rb.MovePosition(Vector2.MoveTowards(rb.position, player.position, chaseSpeed * Time.deltaTime));

        // Flip sprite based on direction
        spriteRenderer.flipX = player.position.x > rb.position.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boss collided with the player!");
            if (damageRoutine == null)
            {
                damageRoutine = StartCoroutine(DealDamageOverTime(collision.gameObject.GetComponent<PlayerHealth>()));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Player took " + damageAmount + " damage!");
            yield return new WaitForSeconds(damageInterval);
        }
    }
}