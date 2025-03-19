using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI healthText; // Displays player health
    public GameObject pauseMenu; // Pause menu UI
    public GameObject winScreen; // Winning screen UI
    public GameObject loseScreen; // Losing screen UI

    private bool isPaused = false;
    private PlayerHealth playerHealth;
    private BossHealth bossHealth; // Reference to the boss's health

    [Header("Audio")]
    public AudioSource bgmSource; // Background music audio source
    public AudioClip victoryMusic; // Assign in Inspector
    public AudioClip gameOverMusic; // Assign in Inspector

    private bool hasLost = false;

    void Start()
    {
        Time.timeScale = 1f; // Reset time scale before reloading
        playerHealth = FindObjectOfType<PlayerHealth>();
        bossHealth = FindObjectOfType<BossHealth>();

        UpdateHealthUI();
        pauseMenu.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    void Update()
    {
        // Toggle pause menu when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Update player health UI
        if (playerHealth != null)
        {
            UpdateHealthUI();

            // Check if player is dead
            if (playerHealth.GetCurrentHealth() <= 0)
            {
                ShowLoseScreen();
            }
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
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ShowWinScreen()
    {
        PlayMusic(victoryMusic);
        winScreen.SetActive(true);
        Time.timeScale = 0f; // Stop the game
        
    }

    public void ShowLoseScreen()
    {
        if (hasLost) return; // Ensure it only runs once
        hasLost = true;

        PlayMusic(gameOverMusic);
        loseScreen.SetActive(true);
        Time.timeScale = 0f; // Stop the game
        
    }

    // Button Methods
    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your actual scene name
    }

    private void PlayMusic(AudioClip newMusic)
    {
        if (bgmSource != null && newMusic != null)
        {
            bgmSource.Stop();
            bgmSource.clip = newMusic;
            bgmSource.Play();
        }
    }
}
