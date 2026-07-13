using UnityEngine;
using TMPro;

public class PlayerLapUILevel2 : MonoBehaviour
{
    public TMP_Text lapText;

    private LapCounterLevel2 lapCounter;


    void Start()
    {
        lapCounter =
            GetComponent<LapCounterLevel2>();

        lapText.gameObject.SetActive(true);
    }



    void Update()
    {
        if(lapCounter == null)
            return;


        lapText.text =
            "Lap : "
            + lapCounter.lapCount;
    }
}