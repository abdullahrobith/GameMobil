using UnityEngine;

public class RespawnCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CarRespawn respawn =
            other.GetComponent<CarRespawn>();

        if(respawn != null)
        {
            respawn.SaveCheckpoint(transform);
        }
    }
}