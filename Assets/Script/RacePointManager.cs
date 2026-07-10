using UnityEngine;

public class RacePointManager : MonoBehaviour
{
    public static RacePointManager Instance;

    private Transform[] checkpoints;

    void Awake()
    {
        Instance = this;

        checkpoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            checkpoints[i] = transform.GetChild(i);
        }
    }

    public int TotalCheckpoint()
    {
        return checkpoints.Length;
    }

    public int GetCheckpointIndex(Transform cp)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i] == cp)
                return i;
        }

        return -1;
    }
}