using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IMovementProvider
{
    public NavMeshAgent agent;
    private UnitStatsManager stats;
    public float CurrentSpeed => agent != null ? agent.velocity.magnitude : 0f;
    public bool IsMoving => CurrentSpeed > 0.1f;

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
    public bool HasReachedDestination()
    {
        if (agent.pathPending)
            return false;

        return agent.remainingDistance <= agent.stoppingDistance;
    }
}