using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Player"))
        {
            Debug.Log("Player fell! Restarting scene..."); // Debugging
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
