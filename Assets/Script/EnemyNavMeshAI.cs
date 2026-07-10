using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMeshAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private int currentWaypoint = 0;

    private bool started = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (WaypointManager.Instance == null)
        {
            Debug.LogError("WaypointManager tidak ditemukan!");
            enabled = false;
            return;
        }

        agent.isStopped = true;
    }

    void Update()
    {
        if (!started)
            return;

        if (WaypointManager.Instance == null)
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
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        started = true;

        agent.isStopped = false;

        agent.SetDestination(
            WaypointManager.Instance.GetWaypoint(currentWaypoint).position);
    }

    public void StopAI()
    {
        started = false;

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        agent.isStopped = true;
        agent.ResetPath();
    }
}