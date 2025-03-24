using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    public Transform pointA, pointB; // Patrol points
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;

    private Transform player;
    private Vector2 targetPosition;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = pointA.position; // Start by moving towards point A
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance < 0.5f) // Increased threshold
        {

            targetPosition = (targetPosition == (Vector2)pointA.position) ? pointB.position : pointA.position;

        }

        MoveToTarget(targetPosition, patrolSpeed);
    }

    void ChasePlayer()
    {
        MoveToTarget(player.position, chaseSpeed);
    }

    void MoveToTarget(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        // Preserve the existing Y velocity (gravity)
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
