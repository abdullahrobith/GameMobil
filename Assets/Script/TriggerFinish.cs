using UnityEngine;

public class TriggerFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerLap player = other.GetComponent<PlayerLap>();
        if (player != null)
        {
            player.CrossFinish();
            return;
        }

        AILap ai = other.GetComponent<AILap>();
        if (ai != null)
        {
            ai.CrossFinish();
        }
    }
}