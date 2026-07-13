using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMeshAI_Level2 : MonoBehaviour
{
    private NavMeshAgent agent;

    private int currentWaypoint = 0;

    bool started = false;

    public int currentLap = 1;
    bool passedMiddle = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (WaypointManager.Instance == null)
        {
            enabled = false;
            return;
        }

        agent.isStopped = true;
    }

    void Update()
    {
        if (!started)
            return;

        if (agent.pathPending)
            return;

        if (agent.remainingDistance <= 2f)
        {
            currentWaypoint =
                WaypointManager.Instance.GetNextIndex(currentWaypoint);

            agent.SetDestination(
                WaypointManager.Instance.GetWaypoint(currentWaypoint).position);
        }
    }

    public void StartAI()
    {
        started = true;

        agent.isStopped = false;

        agent.SetDestination(
            WaypointManager.Instance.GetWaypoint(currentWaypoint).position);
    }

    public void StopAI()
    {
        started = false;

        agent.isStopped = true;

        agent.ResetPath();
    }

    public void PassedMiddle()
    {
        passedMiddle = true;
    }

    public void CrossFinish()
    {
        if (!passedMiddle)
            return;

        passedMiddle = false;

        currentLap++;

        Debug.Log(name + " Lap " + currentLap);
    }
}