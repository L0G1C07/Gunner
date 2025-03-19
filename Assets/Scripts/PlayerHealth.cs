using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;

    public AudioClip hurtSound; // Assign in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            StartCoroutine(Invincibility());

            if (spriteRenderer != null)
            {
                StartCoroutine(FlashRed());
            }

            if (hurtSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hurtSound);
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = damageColor; // Turn red
        yield return new WaitForSeconds(0.2f); // Keep red for a short time
        spriteRenderer.color = originalColor; // Revert to original color
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps") || collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
