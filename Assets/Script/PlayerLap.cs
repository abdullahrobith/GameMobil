using UnityEngine;
using TMPro;

public class PlayerLap : MonoBehaviour
{
    public TMP_Text lapText;
    public GameObject finishPanel;

    public int maxLap = 3;

    int currentLap = 1;
    bool passedMiddle = false;
    bool raceFinished = false;

    void Start()
    {
        lapText.gameObject.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void ShowLapUI()
    {
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

        if (currentLap > maxLap)
        {
            raceFinished = true;

            int rank =
                RaceRanking.Instance.RegisterFinish("KAMU");

            finishPanel.SetActive(true);

            Time.timeScale = 0;

            return;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        lapText.text = "Lap " + currentLap + "/" + maxLap;
    }
}