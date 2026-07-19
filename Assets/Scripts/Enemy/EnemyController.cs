using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform towerTarget;

    private NavMeshAgent agent;
    private PlayerDetector detector;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<PlayerDetector>();
    }

    private void Update()
    {
        if (detector.DetectedPlayer != null)
        {
            agent.SetDestination(detector.DetectedPlayer.position);
        }
        else
        {
            agent.SetDestination(towerTarget.position);
        }
    }
}