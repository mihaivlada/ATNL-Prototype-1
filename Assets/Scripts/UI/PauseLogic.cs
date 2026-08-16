using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Helper.isPaused = true; // Set the pause state to true
    }

    public void Home() 
    { 
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Resume the game
        Helper.isPaused = false; // Set the pause state to false
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        Helper.isPaused = false; // Set the pause state to false
    }

    public void Restart() 
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Resume the game
        Helper.isPaused = false; // Set the pause state to false
    }
}
