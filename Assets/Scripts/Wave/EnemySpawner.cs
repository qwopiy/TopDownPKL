using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Database")]
    [SerializeField] private EnemyTierSO enemyTierSO;

    [Header("Spawn Point")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Setting")]
    [SerializeField] private float spawnInterval = 0.5f;

    public void SpawnWave(int enemyCount, WaveDifficulty difficulty)
    {
        StartCoroutine(SpawnCoroutine(enemyCount, difficulty));
    }

    private IEnumerator SpawnCoroutine(int enemyCount, WaveDifficulty difficulty)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(difficulty);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(WaveDifficulty difficulty)
    {
        List<GameObject> selectedTier =
            WeightedRandomPicker.GetRandomTier(enemyTierSO, difficulty);

        if (selectedTier == null || selectedTier.Count == 0)
        {
            Debug.LogWarning("Enemy Tier kosong.");
            return;
        }

        GameObject prefab =
            selectedTier[Random.Range(0, selectedTier.Count)];

        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        WaveManager.Instance.EnemySpawned();
    }
}