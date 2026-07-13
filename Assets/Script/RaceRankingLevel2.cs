using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class RaceRankingLevel2 : MonoBehaviour
{
    public TMP_Text rankingText;


    public List<LapCounterLevel2> racers =
        new List<LapCounterLevel2>();



    public void ShowRanking()
    {
        var sorted =
            racers
            .OrderByDescending(x => x.lapCount)
            .ToList();



        rankingText.text = "";



        for(int i = 0; i < sorted.Count; i++)
        {
            if (sorted[i].racerName == "KAMU")
        {
            rankingText.text +=
                "<color=#FFD700>"
                + (i + 1)
                + ". "
                + sorted[i].racerName
                + " - "
                + sorted[i].lapCount
                + " Lap"
                + "</color>\n";
        }
        else
        {
            rankingText.text +=
                (i + 1)
                + ". "
                + sorted[i].racerName
                + " - "
                + sorted[i].lapCount
                + " Lap\n";
        }
        }
    }
}