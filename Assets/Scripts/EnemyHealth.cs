using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5; // Set enemy health
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor; // Turn red
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor; // Reset color
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject); // Destroy enemy
    }
}
