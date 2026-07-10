using UnityEngine;

public class LapCounter : MonoBehaviour
{
    [Header("Race")]
    public int totalLap = 3;

    [Header("Read Only")]
    public int currentLap = 1;
    public int currentCheckpoint = 0;
    public bool finished = false;

    void Update()
    {
        if (finished)
            return;

        if (WaypointManager.Instance == null)
            return;

        if (WaypointManager.Instance.ReachedCheckpoint(
            currentCheckpoint,
            transform.position))
        {
            currentCheckpoint++;

            if (currentCheckpoint >= WaypointManager.Instance.TotalWaypoint())
            {
                currentCheckpoint = 0;

                currentLap++;

                Debug.Log(gameObject.name +
                    " Lap : " + currentLap);

                if (currentLap > totalLap)
                {
                    finished = true;

                    Debug.Log(gameObject.name + " FINISH");
                }
            }
        }
    }

    public float GetProgress()
    {
        return currentLap *
               WaypointManager.Instance.TotalWaypoint()
               + currentCheckpoint;
    }
}