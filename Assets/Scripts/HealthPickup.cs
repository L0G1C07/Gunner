using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Heal the player
                Debug.Log("Player healed by " + healAmount);
                Destroy(gameObject); // Remove the pickup after use
            }
        }
    }
}
