using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(TargetFinder))]
public class PatrolController : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase
    }

    [Header("Patrol Settings")]
    [SerializeField] private float waypointReachDistance = 0.3f;

    public PatrolRoute patrolRoute;
    private EnemyMovement movement;
    private TargetFinder targetFinder;
    private PatrolSpawner spawner;
    public PatrolSpawner Spawner => spawner;

    private int currentWaypoint;
    public State currentState;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        targetFinder = GetComponent<TargetFinder>();
    }

    private void Update()
    {
        if (patrolRoute == null)
            return;

        switch (currentState)
        {
            case State.Patrol:
                UpdatePatrol();
                break;

            case State.Chase:
                UpdateChase();
                break;
        }
    }

    public void Initialize(PatrolRoute route, int startWaypoint, PatrolSpawner patrolSpawner)
    {
        patrolRoute = route;
        spawner = patrolSpawner;

        currentWaypoint = Mathf.Clamp(startWaypoint, 0, patrolRoute.Waypoints.Length - 1);

        currentState = State.Patrol;

        MoveToCurrentWaypoint();
    }

    private void UpdatePatrol()
    {
        if (targetFinder.NearestTarget != null)
        {
            currentState = State.Chase;
            return;
        }

        Transform waypoint = patrolRoute.Waypoints[currentWaypoint];

        if (movement.HasReachedDestination())
        {
            currentWaypoint++;

            if (currentWaypoint >= patrolRoute.Waypoints.Length)
                currentWaypoint = 0;

            MoveToCurrentWaypoint();
        }
    }

    private void UpdateChase()
    {
        if (targetFinder.NearestTarget == null)
        {
            currentState = State.Patrol;
            MoveToCurrentWaypoint();
            return;
        }

        movement.MoveTo(targetFinder.NearestTarget.position);
    }

    private void MoveToCurrentWaypoint()
    {
        movement.MoveTo(patrolRoute.Waypoints[currentWaypoint].position);
    }
}