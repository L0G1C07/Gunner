using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagTutorial : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip pickupSound; // Assign in Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player fell! Restarting scene..."); // Debugging
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            SceneManager.LoadScene("Game");
        }
    }
}
