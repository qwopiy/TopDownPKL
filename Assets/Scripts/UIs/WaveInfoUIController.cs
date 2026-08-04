using System.Collections;
using TMPro;
using UnityEngine;

public class WaveInfoUIController : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1f);
    [Header("References")]
    public TextMeshProUGUI waveCount;
    public TextMeshProUGUI waveInfo;
    public TextMeshProUGUI waveDuration;
    private void Start()
    {
        WaveManager.Instance.WaveStarted += UpdateWaveCount;
        WaveManager.Instance.WaveRunning += UpdateWaveInfo;
        WaveManager.Instance.WaitUntilNextWave += StartWaveCountdown;

        WaveManager.Instance.AnEnemyDied += UpdateCurrentEnemies;
    }

    private void OnDisable()
    {
        WaveManager.Instance.WaveStarted -= UpdateWaveCount;
        WaveManager.Instance.WaveRunning -= UpdateWaveInfo;
        WaveManager.Instance.WaitUntilNextWave -= StartWaveCountdown;

        WaveManager.Instance.AnEnemyDied -= UpdateCurrentEnemies;
    }
    public void UpdateWaveCount(int currentWave)
    {
        waveCount.text = $"Current Wave: {currentWave}";
    }

    public void UpdateWaveInfo(bool waveRunning)
    {
        if (!waveRunning) 
        {
            waveInfo.text = "Preparing for wave...";
        }
        else
        {
            waveInfo.text = "Get Ready!";
        }   
    }

    public void UpdateCurrentEnemies()
    {
        int enemiesAlive = WaveManager.Instance.EnemiesAlive;
        waveInfo.text = $"Enemies Alive: {enemiesAlive}";
    }
    public void StartWaveCountdown(int duration)
    {
        StartCoroutine(WaveCountdownRoutine(duration));
    }

    private IEnumerator WaveCountdownRoutine(int duration)
    {
        int remainingTime = duration;
        while (remainingTime > 0)
        {
            waveDuration.text = $"Next Wave In: {remainingTime:F1} seconds";
            yield return _waitForSeconds1;
            remainingTime--;
        }
        waveDuration.text = "Wave Started!";
    }
}
