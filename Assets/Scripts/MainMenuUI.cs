using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Assign the Main Menu Panel in Inspector
    public GameObject howToPlayPanel; // Assign the How to Play Panel in Inspector
    public GameObject creditsPanel;   // Assign the Credits Panel in Inspector

    void Start()
    {
        // Ensure the panels are hidden when the game starts
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true); // Main menu starts enabled
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Change to your actual scene name
    }

    public void ShowHowToPlay()
    {
        mainMenuPanel.SetActive(false);  // Disable Main Menu
        howToPlayPanel.SetActive(true);  // Show How to Play Panel
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);  // Disable Main Menu
        creditsPanel.SetActive(true);    // Show Credits Panel
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false); // Hide How to Play Panel
        mainMenuPanel.SetActive(true);   // Enable Main Menu
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);   // Hide Credits Panel
        mainMenuPanel.SetActive(true);   // Enable Main Menu
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // This log appears in the Unity Editor
    }
}
