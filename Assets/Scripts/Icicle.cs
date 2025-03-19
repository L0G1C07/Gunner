using UnityEngine;

public class Icicle : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    private AudioSource audioSource;
    public AudioClip fallSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity initially
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name); // Debugging

        if (!hasFallen && other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Icicle falling.");
            hasFallen = true;
            rb.gravityScale = 1; // Enable gravity to make it fall
            if (fallSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fallSound);
            }
        }
    }
}
