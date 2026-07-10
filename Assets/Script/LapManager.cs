using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    [Header("Lap")]
    public int currentLap = 1;
    public int maxLap = 3;

    [Header("UI")]
    public TMP_Text lapText;
    public GameObject finishPanel;

    private bool passedMiddle = false;
    private bool raceFinished = false;

    private void Start()
    {
        UpdateLapUI();

        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    public void PassedMiddle()
    {
        if (raceFinished) return;

        passedMiddle = true;
        Debug.Log("Checkpoint Tengah dilewati");
    }

    public void CrossFinish()
    {
        if (raceFinished) return;

        if (!passedMiddle)
        {
            Debug.Log("Belum melewati checkpoint tengah");
            return;
        }

        passedMiddle = false;
        currentLap++;

        if (currentLap > maxLap)
        {
            raceFinished = true;

            Debug.Log("FINISH");

            if (finishPanel != null)
                finishPanel.SetActive(true);

            return;
        }

        UpdateLapUI();

        Debug.Log("Lap " + currentLap);
    }

    void UpdateLapUI()
    {
        if (lapText != null)
            lapText.text = "Lap " + currentLap + "/" + maxLap;
    }
}