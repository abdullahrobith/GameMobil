using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    [Header("Timer UI")]
    public TMP_Text timerText;


    [Header("Race Time")]
    public float raceTime = 300f;


    [Header("Finish Result")]
    public GameObject finishPanel;
    public RaceRankingLevel2 ranking;

    [Header("Mobile UI")]
    public GameObject mobileControl;


    private float currentTime;
    private bool isRunning = false;


    public static bool RaceFinished = false;



    void Start()
    {
        currentTime = raceTime;


        // sembunyikan timer awal
        if(timerText != null)
            timerText.gameObject.SetActive(false);



        // sembunyikan panel finish awal
        if(finishPanel != null)
            finishPanel.SetActive(false);

        if(mobileControl != null)
        mobileControl.SetActive(false);



        UpdateUI();
    }





    void Update()
    {
        if(!isRunning)
            return;



        currentTime -= Time.deltaTime;



        if(currentTime <= 0)
        {
            currentTime = 0;


            isRunning = false;


            RaceFinished = true;


            UpdateUI();



            FinishRace();


            return;
        }



        UpdateUI();
    }





    void UpdateUI()
    {
        if(timerText == null)
            return;



        int minute =
            Mathf.FloorToInt(currentTime / 60);



        int second =
            Mathf.FloorToInt(currentTime % 60);



        timerText.text =
            minute.ToString("00")
            + ":"
            +
            second.ToString("00");
    }





    public void StartTimer()
    {
        if(timerText != null)
            timerText.gameObject.SetActive(true);
        
        if(mobileControl != null)
            mobileControl.SetActive(true);



        RaceFinished = false;


        isRunning = true;
    }





    void FinishRace()
    {
        Debug.Log("TIME UP");



        // =========================
        // STOP PLAYER
        // =========================

        CarController player =
            FindFirstObjectByType<CarController>();


        if(player != null)
        {
            player.enabled = false;
        }





        // =========================
        // STOP AI
        // =========================

        EnemyNavMeshAI[] ai =
            FindObjectsByType<EnemyNavMeshAI>(
                FindObjectsSortMode.None);



        foreach(EnemyNavMeshAI bot in ai)
        {
            if(bot != null)
                bot.StopAI();
        }





        // =========================
        // HITUNG RANKING
        // =========================

        if(ranking != null)
        {
            ranking.ShowRanking();
        }
        else
        {
            Debug.LogWarning(
                "RaceRankingLevel2 belum dihubungkan!"
            );
        }


        if(mobileControl != null)
        {
            mobileControl.SetActive(false);
        }


        // =========================
        // TAMPILKAN PANEL
        // =========================

        if(finishPanel != null)
        {
            finishPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "Finish Panel belum dihubungkan!"
            );
        }
    }
}