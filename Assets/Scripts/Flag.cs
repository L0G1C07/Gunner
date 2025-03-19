using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if player touches the flag
        {
            FindObjectOfType<GameUIManager>().ShowWinScreen();
            Destroy(gameObject); // Remove the flag after winning
        }
    }
}
