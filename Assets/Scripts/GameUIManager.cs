using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Displays player health
    public GameObject pauseMenu; // Pause menu UI

    private bool isPaused = false;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        UpdateHealthUI();
        pauseMenu.SetActive(false); // Ensure the pause menu is hidden at start
    }

    void Update()
    {
        // Toggle pause menu when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Ensure UI updates correctly if health changes
        if (playerHealth != null)
        {
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null && playerHealth != null)
        {
            healthText.text = "" + playerHealth.GetCurrentHealth();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f; // Freeze/unfreeze game
    }

    // Button Methods
    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure game is running before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your actual scene name
    }
}
