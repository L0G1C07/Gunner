using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector
    public Transform firePoint; // The position where bullets will spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;

    private bool isFacingRight = true; // Make sure this matches your movement script


    public AudioClip shootSound; // Assign the shooting sound in the Inspector
    private AudioSource audioSource; // Reference to AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void Update()
    {
        // Get player direction from transform scale or movement script
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            isFacingRight = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            isFacingRight = false;
        }

        // Fire bullet when pressing Fire1 (default: left mouse button or Ctrl)
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set next fire time
        }
    }

    void Shoot()
    {
        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Ensure the bullet doesn't become a child of the player
        bullet.transform.parent = null;

        // Set bullet direction based on facing direction
        float direction = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * bulletSpeed, 0f);

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

}
