using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Wave Settings")]
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int baseEnemyCount = 6;
    [SerializeField] private int enemyIncreasePerWave = 1;
    [SerializeField] private float preparationTime = 30f;


    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveRunning = false;

    public int CurrentWave => currentWave;
    public event Action <bool> WaveRunning;
    public WaveDifficulty CurrentDifficulty { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(GameStartRoutine());
    }
    private IEnumerator GameStartRoutine()
    {
        Debug.Log("Preparation Started");

        yield return new WaitForSeconds(preparationTime);

        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        waveRunning = true;
        WaveRunning?.Invoke(waveRunning);

        CurrentDifficulty = WaveDifficultyCalculator.GetDifficulty(currentWave);

        int enemyCount = baseEnemyCount + ((currentWave - 1) * enemyIncreasePerWave);

        Debug.Log($"Wave {currentWave} Started");

        enemySpawner.SpawnWave(enemyCount, CurrentDifficulty);
    }

    public void EnemySpawned()
    {
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (waveRunning && enemiesAlive <= 0)
        {
            waveRunning = false;

            Debug.Log($"Wave {currentWave} Completed");

            StartCoroutine(StartNextWave());
        }
    }
}