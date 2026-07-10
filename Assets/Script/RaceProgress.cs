using UnityEngine;

public class RaceProgress : MonoBehaviour
{
    [Header("Race")]
    public int totalLap = 3;

    public int currentLap = 1;

    public int currentCheckpoint = 0;

    public bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Masuk Trigger : " + other.name);
        Debug.Log("RacePointManager = " + RacePointManager.Instance);
        int checkpoint =
            RacePointManager.Instance.GetCheckpointIndex(other.transform);

        Debug.Log("Index Trigger = " + checkpoint);
        Debug.Log("CurrentCheckpoint = " + currentCheckpoint);

        if (checkpoint == -1)
        {
            Debug.Log("Checkpoint tidak ditemukan");
            return;
        }

        if (checkpoint != currentCheckpoint)
        {
            Debug.Log("Bukan checkpoint berikutnya");
            return;
        }

        currentCheckpoint++;

        Debug.Log("Checkpoint berhasil -> " + currentCheckpoint);

        if (currentCheckpoint >= RacePointManager.Instance.TotalCheckpoint())
        {
            currentCheckpoint = 0;

            currentLap++;

            Debug.Log("Lap bertambah " + currentLap);
        }
    }
}