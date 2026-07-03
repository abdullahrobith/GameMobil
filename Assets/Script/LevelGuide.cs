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
    public EnemyAI[] enemyAI;

    void Start()
    {
        // Tampilkan panel guide
        guidePanel.SetActive(true);

        // Sembunyikan countdown
        countdownObject.SetActive(false);

        // Matikan kontrol player
        if (carController != null)
            carController.enabled = false;

        // Matikan semua AI
        foreach (EnemyAI ai in enemyAI)
        {
            if (ai != null)
                ai.enabled = false;
        }

        // Pause game
        Time.timeScale = 0f;
    }

    public void StartLevel()
    {
        // Tutup guide
        guidePanel.SetActive(false);

        // Lanjutkan game
        Time.timeScale = 1f;

        // Mulai countdown
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownObject.SetActive(true);

        yield return StartCoroutine(AnimateNumber("3", Color.white));

        yield return StartCoroutine(AnimateNumber("2", Color.white));

        yield return StartCoroutine(AnimateNumber("1", Color.white));

        yield return StartCoroutine(AnimateGO());

        // Aktifkan Player
        if (carController != null)
            carController.enabled = true;

        // Aktifkan semua Bot
        foreach (EnemyAI ai in enemyAI)
        {
            if (ai != null)
                ai.enabled = true;
        }

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

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            t = 1f - Mathf.Pow(1f - t, 3);

            countdownText.transform.localScale =
                Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        countdownText.transform.localScale = endScale;

        yield return new WaitForSeconds(0.55f);
    }

    IEnumerator AnimateGO()
    {
        countdownText.text = "GO!";

        countdownText.color = new Color32(0, 255, 120, 255);

        float timer = 0f;

        while (timer < 0.45f)
        {
            timer += Time.deltaTime;

            float scale =
                1f + Mathf.Sin(timer * 15f) * 0.25f;

            countdownText.transform.localScale =
                Vector3.one * scale;

            yield return null;
        }

        countdownText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.5f);
    }
}