using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private UnitStatsManager stats;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<UnitStatsManager>();

        agent.speed = stats.CurrentMovementSpeed;
        agent.angularSpeed = stats.CurrentRotationSpeed;
        agent.stoppingDistance = stats.CurrentAttackRange;
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