using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialMenu; // Assign the tutorial panel in the Inspector

    void Start()
    {
        Time.timeScale = 0; // Pause the game at the start
        tutorialMenu.SetActive(true); // Show the tutorial menu
    }

    public void PlayTutorial()
    {
        Time.timeScale = 1; // Resume the game
        tutorialMenu.SetActive(false); // Hide tutorial menu
    }

    public void SkipToGame()
    {
        Time.timeScale = 1; // Resume the game before switching
        SceneManager.LoadScene("Game"); // Replace with your actual game scene name
    }
}
