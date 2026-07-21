using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "ScriptableObjects/Wave Data")]
public class WaveDataSO : ScriptableObject
{
    public EnemySpawnData[] enemies;
}


[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;

    public int amount;

    public float spawnDelay;
}