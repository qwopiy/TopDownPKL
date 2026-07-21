using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatsManager))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStatsManager stats;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStatsManager>();

        agent.speed = stats.CurrentMovementSpeed;
        agent.acceleration = stats.CurrentAcceleration;
        agent.angularSpeed = stats.CurrentRotationSpeed;
        agent.stoppingDistance = stats.CurrentStoppingDistance;
        agent.autoBraking = stats.CurrentAutoBraking;
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        agent.ResetPath();
    }
}