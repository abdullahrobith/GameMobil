using UnityEngine;

public class LapCounterLevel2 : MonoBehaviour
{
    public string racerName;

    public int lapCount = 0;

    private bool passedMiddle = false;



    void Start()
    {
        if(string.IsNullOrEmpty(racerName))
        {
            racerName = gameObject.name;
        }
    }



    public void PassedMiddle()
    {
        passedMiddle = true;
    }



    public void CrossFinish()
    {
        if(!passedMiddle)
            return;


        passedMiddle = false;


        lapCount++;


        Debug.Log(
            racerName +
            " Lap : " +
            lapCount
        );
    }
}