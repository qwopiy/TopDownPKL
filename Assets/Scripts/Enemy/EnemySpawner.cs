using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;


    public IEnumerator SpawnEnemy(
        GameObject enemyPrefab,
        int amount,
        float delay)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(delay);
        }
    }
}