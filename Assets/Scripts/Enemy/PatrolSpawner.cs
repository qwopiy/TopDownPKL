using UnityEngine;

public class PatrolSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Patrol")]
    [SerializeField] private PatrolRoute patrolRoute;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            transform.position,
            transform.rotation);

        PatrolController patrol = enemy.GetComponent<PatrolController>();

        if (patrol != null)
        {
            int randomWaypoint = Random.Range(0, patrolRoute.Waypoints.Length);

            patrol.Initialize(patrolRoute, randomWaypoint);
        }
    }
}