using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveDataSO[] waves;

    [SerializeField] private EnemySpawner spawner;


    private int currentWave = 0;


    private void Start()
    {
        StartCoroutine(StartWave());
    }


    private IEnumerator StartWave()
    {
        WaveDataSO wave = waves[currentWave];


        foreach (var enemy in wave.enemies)
        {
            yield return StartCoroutine(
                spawner.SpawnEnemy(
                    enemy.enemyPrefab,
                    enemy.amount,
                    enemy.spawnDelay
                )
            );
        }


        Debug.Log("Wave selesai");
    }
}