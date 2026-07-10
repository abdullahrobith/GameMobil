using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyNavMeshAI))]
[RequireComponent(typeof(NavMeshAgent))]
public class AILap : MonoBehaviour
{
    public int maxLap = 3;

    private int currentLap = 1;
    private bool passedMiddle = false;
    private bool finished = false;

    private EnemyNavMeshAI ai;
    private NavMeshAgent agent;

    void Awake()
    {
        ai = GetComponent<EnemyNavMeshAI>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void PassedMiddle()
    {
        if (finished)
            return;

        passedMiddle = true;
    }

    public void CrossFinish()
    {
        if (finished)
            return;

        if (!passedMiddle)
            return;

        passedMiddle = false;

        currentLap++;

        Debug.Log(name + " Lap " + currentLap);

        if (currentLap > maxLap)
        {
            finished = true;
            RaceRanking.Instance.RegisterFinish(gameObject.name);

            Debug.Log(name + " FINISH");

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            if (ai != null)
            {
                ai.enabled = false;
            }
        }
    }
}