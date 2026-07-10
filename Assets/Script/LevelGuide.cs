using UnityEngine;
using TMPro;
using System.Collections;

public class LevelGuide : MonoBehaviour
{
    [Header("Guide")]
    public GameObject guidePanel;


    [Header("Countdown")]
    public GameObject countdownObject;
    public TMP_Text countdownText;


    [Header("Player")]
    public CarController carController;


    [Header("Enemy AI")]
    public EnemyNavMeshAI[] enemyAI;


    [Header("HUD")]
    public GameObject lapPanel;
    public GameObject speedPanel;


    [Header("Mobile Control")]
    public GameObject gasButton;
    public GameObject brakeButton;



    void Start()
    {
        // tampilkan guide
        guidePanel.SetActive(true);


        // sembunyikan countdown
        countdownObject.SetActive(false);



        // sembunyikan HUD awal
        if (lapPanel != null)
            lapPanel.SetActive(false);


        if (speedPanel != null)
            speedPanel.SetActive(false);



        // sembunyikan tombol mobil
        if (gasButton != null)
            gasButton.SetActive(false);


        if (brakeButton != null)
            brakeButton.SetActive(false);



        // matikan kontrol mobil
        if (carController != null)
            carController.enabled = false;



        // matikan AI
        foreach (EnemyNavMeshAI ai in enemyAI)
        {
            if (ai != null)
                ai.StopAI();
        }



        Time.timeScale = 0f;
    }





    public void StartLevel()
    {
        // tutup guide
        guidePanel.SetActive(false);


        // lanjutkan waktu
        Time.timeScale = 1f;


        // mulai countdown
        StartCoroutine(StartCountdown());
    }





    IEnumerator StartCountdown()
    {
        countdownObject.SetActive(true);



        yield return StartCoroutine(
            AnimateNumber("3", Color.white)
        );


        yield return StartCoroutine(
            AnimateNumber("2", Color.white)
        );


        yield return StartCoroutine(
            AnimateNumber("1", Color.white)
        );


        yield return StartCoroutine(
            AnimateGO()
        );



        // ============================
        // MULAI GAME
        // ============================


        // aktifkan player
        if(carController != null)
            carController.enabled = true;



        // aktifkan AI
        foreach(EnemyNavMeshAI ai in enemyAI)
        {
            if(ai != null)
                ai.StartAI();
        }



        // tampilkan HUD
        if(lapPanel != null)
            lapPanel.SetActive(true);


        if(speedPanel != null)
            speedPanel.SetActive(true);



        // tampilkan tombol kontrol
        if(gasButton != null)
            gasButton.SetActive(true);


        if(brakeButton != null)
            brakeButton.SetActive(true);



        yield return new WaitForSeconds(0.3f);



        countdownObject.SetActive(false);
    }





    IEnumerator AnimateNumber(string number, Color color)
    {
        countdownText.text = number;

        countdownText.color = color;



        Vector3 startScale = Vector3.one * 2.5f;

        Vector3 endScale = Vector3.one;



        float timer = 0f;

        float duration = 0.35f;



        while(timer < duration)
        {
            timer += Time.deltaTime;



            float t = timer / duration;

            t = 1f - Mathf.Pow(1f - t, 3);



            countdownText.transform.localScale =
                Vector3.Lerp(
                    startScale,
                    endScale,
                    t
                );



            yield return null;
        }



        countdownText.transform.localScale = endScale;



        yield return new WaitForSeconds(0.55f);
    }





    IEnumerator AnimateGO()
    {
        countdownText.text = "GO!";


        countdownText.color =
            new Color32(
                0,
                255,
                120,
                255
            );



        float timer = 0f;



        while(timer < 0.45f)
        {
            timer += Time.deltaTime;



            float scale =
                1f +
                Mathf.Sin(timer * 15f)
                * 0.25f;



            countdownText.transform.localScale =
                Vector3.one * scale;



            yield return null;
        }



        countdownText.transform.localScale =
            Vector3.one;



        yield return new WaitForSeconds(0.5f);
    }
}