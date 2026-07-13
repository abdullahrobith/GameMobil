using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    // Restart level yang sedang dimainkan
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Kembali ke Main Menu
    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    // Pindah ke level berikutnya
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // Jika masih ada level berikutnya
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            // Jika sudah level terakhir, kembali ke Main Menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}