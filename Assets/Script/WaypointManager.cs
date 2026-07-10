using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    [HideInInspector]
    public Transform[] waypoints;

    [Header("Checkpoint")]
    public float checkpointRadius = 4f;

    void Awake()
    {
        Instance = this;

        LoadWaypoints();
    }

    void LoadWaypoints()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    public Transform GetWaypoint(int index)
    {
        if (waypoints == null || waypoints.Length == 0)
            return null;

        return waypoints[index % waypoints.Length];
    }

    public int GetNextIndex(int index)
    {
        if (waypoints.Length == 0)
            return 0;

        return (index + 1) % waypoints.Length;
    }

    public bool ReachedCheckpoint(int index, Vector3 position)
    {
        if (waypoints == null || waypoints.Length == 0)
            return false;

        float distance =
            Vector3.Distance(
                position,
                GetWaypoint(index).position);

        return distance <= checkpointRadius;
    }

    void OnDrawGizmos()
    {
        if (transform.childCount == 0)
            return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform current = transform.GetChild(i);
            Transform next = transform.GetChild((i + 1) % transform.childCount);

            Gizmos.DrawSphere(current.position, checkpointRadius);

            Gizmos.DrawLine(
                current.position,
                next.position);
        }
    }

    public int TotalWaypoint()
    {
        return waypoints.Length;
    }

    public bool CheckCheckpoint(int checkpointIndex, Vector3 carPosition)
    {
        float distance =
            Vector3.Distance(
                carPosition,
                GetWaypoint(checkpointIndex).position);

        return distance <= checkpointRadius;
    }
}