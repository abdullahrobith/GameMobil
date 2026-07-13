using UnityEngine;
using TMPro;

public class PlayerLap : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameplayUI;
    public TMP_Text lapText;
    public GameObject finishPanel;

    [Header("Race")]
    public int maxLap = 3;

    private int currentLap = 1;
    private bool passedMiddle = false;
    private bool raceFinished = false;

    void Start()
    {
        if (lapText != null)
            lapText.gameObject.SetActive(false);

        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    public void ShowLapUI()
    {
        if (lapText != null)
            lapText.gameObject.SetActive(true);

        UpdateUI();
    }

    public void PassedMiddle()
    {
        passedMiddle = true;
    }

    public void CrossFinish()
    {
        if (raceFinished)
            return;

        if (!passedMiddle)
            return;

        passedMiddle = false;

        currentLap++;

        // Sudah menyelesaikan semua lap
        if (currentLap > maxLap)
        {
            raceFinished = true;

            RaceRanking.Instance.RegisterFinish("KAMU");

            // Sembunyikan Gameplay UI
            if (gameplayUI != null)
                gameplayUI.SetActive(false);

            // Tampilkan Finish Panel
            if (finishPanel != null)
                finishPanel.SetActive(true);

            // Pause game
            Time.timeScale = 0f;

            return;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (lapText == null)
            return;

        lapText.text = "Lap " + currentLap + "/" + maxLap;
    }
}