using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(TargetFinder))]
public class PatrolController : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Wait,
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

    [Header("Idle Settings")]
    [SerializeField] private float minIdleTime = 5f;
    [SerializeField] private float maxIdleTime = 10f;
    private float idleTimer;

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

            case State.Wait:
                UpdateWait();
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
            currentState = State.Wait;

            idleTimer = Random.Range(minIdleTime, maxIdleTime);

            movement.Stop();
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

    private void UpdateWait()
    {
        if (targetFinder.NearestTarget != null)
        {
            currentState = State.Chase;
            return;
        }

        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            currentWaypoint++;

            if (currentWaypoint >= patrolRoute.Waypoints.Length)
                currentWaypoint = 0;

            currentState = State.Patrol;

            MoveToCurrentWaypoint();
        }
    }
}