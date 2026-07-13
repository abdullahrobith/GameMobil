using UnityEngine;

public class TriggerCheckpointLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LapCounterLevel2 lap =
            other.GetComponent<LapCounterLevel2>();


        if(lap != null)
        {
            lap.PassedMiddle();
        }
    }
}