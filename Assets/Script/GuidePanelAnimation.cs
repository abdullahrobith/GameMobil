using System.Collections;
using UnityEngine;

public class GuidePanelAnimation : MonoBehaviour
{
    public RectTransform contentPanel;

    public CanvasGroup title;
    public CanvasGroup description;
    public CanvasGroup button;

    void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        // Posisi awal
        contentPanel.localScale = Vector3.one * 0.8f;
        contentPanel.anchoredPosition = new Vector2(0, -350);

        title.alpha = 0;
        description.alpha = 0;
        button.alpha = 0;

        // Panel masuk
        float t = 0;

        while (t < 1.2f)
        {
            t += Time.unscaledDeltaTime;

            float p = Mathf.SmoothStep(0,1,t/1.2f);

            contentPanel.localScale =
                Vector3.Lerp(
                    Vector3.one*0.8f,
                    Vector3.one,
                    p);

            contentPanel.anchoredPosition =
                Vector2.Lerp(
                    new Vector2(0,-350),
                    Vector2.zero,
                    p);

            yield return null;
        }

        yield return StartCoroutine(FadeCanvas(title));
        yield return new WaitForSecondsRealtime(0.4f);

        yield return StartCoroutine(FadeCanvas(description));
        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(PopButton());
    }

    IEnumerator FadeCanvas(CanvasGroup cg)
    {
        float t = 0;

        while(t < 0.8f)
        {
            t += Time.unscaledDeltaTime;

            cg.alpha = t/0.8f;

            yield return null;
        }

        cg.alpha = 1;
    }

    IEnumerator PopButton()
    {
        button.alpha = 1;

        button.transform.localScale = Vector3.zero;

        float t = 0;

        while(t < 0.6f)
        {
            t += Time.unscaledDeltaTime;

            float p = Mathf.SmoothStep(0,1,t/0.6f);

            button.transform.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one,
                    p);

            yield return null;
        }

        button.transform.localScale = Vector3.one;
    }
}