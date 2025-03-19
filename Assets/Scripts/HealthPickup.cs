using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored

    private AudioSource audioSource;
    public AudioClip pickupSound; // Assign in Inspector

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Heal the player
                Debug.Log("Player healed by " + healAmount);
                if (pickupSound != null && audioSource != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }
                Destroy(gameObject); // Remove the pickup after use
            }
        }
    }
}
