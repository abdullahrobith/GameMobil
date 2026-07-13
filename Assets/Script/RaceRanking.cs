using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceRanking : MonoBehaviour
{
    public static RaceRanking Instance;

    public TMP_Text rankingText;

    private List<string> finishOrder = new List<string>();

    void Awake()
    {
        Instance = this;
    }

    public int RegisterFinish(string racerName)
    {
        if (finishOrder.Contains(racerName))
            return finishOrder.IndexOf(racerName) + 1;

        finishOrder.Add(racerName);

        UpdateUI();

        return finishOrder.Count;
    }

    void UpdateUI()
    {
        if (rankingText == null)
            return;

        rankingText.text = "";

        for (int i = 0; i < finishOrder.Count; i++)
        {
            if (finishOrder[i] == "KAMU")
            {
                rankingText.text +=
                    "<color=#FFD700><b>"
                    + (i + 1)
                    + ". "
                    + finishOrder[i]
                    + "</b></color>\n";
            }
            else
            {
                rankingText.text +=
                    (i + 1)
                    + ". "
                    + finishOrder[i]
                    + "\n";
            }
        }
    }
}