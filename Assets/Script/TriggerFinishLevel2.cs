using UnityEngine;

public class TriggerFinishLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LapCounterLevel2 lap =
            other.GetComponent<LapCounterLevel2>();


        if(lap != null)
        {
            lap.CrossFinish();
        }
    }
}