using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pausePanel;

    [Header("Gameplay UI")]
    public GameObject gameplayUI;


    void Start()
    {
        pausePanel.SetActive(false);
    }


    public void PauseGame()
    {
        pausePanel.SetActive(true);

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        pausePanel.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        Time.timeScale = 1f;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }


    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}