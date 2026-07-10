using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerLap player = other.GetComponent<PlayerLap>();
        if (player != null)
        {
            player.PassedMiddle();
            return;
        }

        AILap ai = other.GetComponent<AILap>();
        if (ai != null)
        {
            ai.PassedMiddle();
        }
    }
}