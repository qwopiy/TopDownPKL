using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private PatrolRoute patrolRoute;

    [SerializeField] private int maxEnemy = 3;

    [SerializeField] private float respawnDelay = 10f;

    private readonly List<GameObject> aliveEnemies = new();

    private void Start()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        aliveEnemies.Add(enemy);

        PatrolController patrol = enemy.GetComponent<PatrolController>();

        if (patrol != null)
        {
            int startWaypoint = Random.Range(0, patrolRoute.Waypoints.Length);

            patrol.Initialize(patrolRoute, startWaypoint, this);
        }
    }

    public void NotifyEnemyDead(GameObject enemy)
    {
        if (!aliveEnemies.Remove(enemy))
            return;

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnEnemy();
    }
}