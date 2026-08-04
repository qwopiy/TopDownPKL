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
    [SerializeField] private int baseEnemyCount = 10;
    [SerializeField] private int enemyIncreasePerWave = 3;
    [SerializeField] private float preparationTime = 30f;


    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveRunning = false;

    public int EnemiesAlive => enemiesAlive;
    public int CurrentWave => currentWave;
    public event Action <bool> WaveRunning;
    public event Action <int> WaveStarted;
    public event Action <int> WaitUntilNextWave;
    public event Action AnEnemyDied;
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
        InitializeInfo();
    }

    private void InitializeInfo()
    {
        WaveStarted?.Invoke(currentWave);
        WaveRunning?.Invoke(waveRunning);
        WaitUntilNextWave?.Invoke((int)timeBetweenWaves + (int)preparationTime);
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
        WaveStarted?.Invoke(currentWave);
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

        AnEnemyDied?.Invoke();

        if (waveRunning && enemiesAlive <= 0)
        {
            waveRunning = false;

            Debug.Log($"Wave {currentWave} Completed");

            WaitUntilNextWave?.Invoke((int)timeBetweenWaves);

            StartCoroutine(StartNextWave());
        }
    }
}