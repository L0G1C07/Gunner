using System.Collections;
using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public float speedMultiplier = 1.5f;  // Speed boost multiplier
    public float bulletSpeedMultiplier = 1.5f; // Bullet speed multiplier
    public float fireRateMultiplier = 0.5f; // Reduces fire rate (faster shooting)
    public float boostDuration = 5f;  // How long the boost lasts

    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Movement playerMovement = other.GetComponent<Movement>();
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();

            if (playerMovement != null && playerShooting != null)
            {
                StartCoroutine(BoostPlayer(playerMovement, playerShooting));
            }

            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Disable visual & collider immediately to prevent multiple triggers
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator BoostPlayer(Movement playerMovement, PlayerShooting playerShooting)
    {
        // Store original values
        float originalSpeed = playerMovement.movementSpeed;
        float originalBulletSpeed = playerShooting.bulletSpeed;
        float originalFireRate = playerShooting.fireRate;

        // Apply Boosts
        playerMovement.movementSpeed *= speedMultiplier;
        playerShooting.bulletSpeed *= bulletSpeedMultiplier;
        playerShooting.fireRate *= fireRateMultiplier; // Fire rate multiplier < 1 = faster shooting

        Debug.Log("Speed & Fire Boost Active!");

        yield return new WaitForSeconds(boostDuration);

        // Reset to original values
        playerMovement.movementSpeed = originalSpeed;
        playerShooting.bulletSpeed = originalBulletSpeed;
        playerShooting.fireRate = originalFireRate;

        Debug.Log("Speed & Fire Boost Ended!");

        Destroy(gameObject); // Now destroy the pickup safely after the boost ends
    }
}
