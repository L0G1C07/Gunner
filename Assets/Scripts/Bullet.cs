using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 3f; // Time before bullet gets destroyed
    public int damage = 1; // Damage dealt to enemies

    void Start()
    {
        Destroy(gameObject, bulletLife); // Destroy after set time
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground") || collision.CompareTag("Traps"))
        {
            // Check if it hit an enemy and apply damage if necessary
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy bullet after hitting
        }
    }
}
