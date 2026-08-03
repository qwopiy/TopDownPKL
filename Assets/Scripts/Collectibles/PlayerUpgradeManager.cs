using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
public class PlayerUpgradeManager : MonoBehaviour
{
    public static PlayerUpgradeManager Instance;

    public int currentCoinCount = 0;
    public int currentUpgradeCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
    }

    private void Init()
    {
        currentCoinCount = 0;
        currentUpgradeCount = 0;
    }

    public void AddCoins(int amount)
    {
        currentCoinCount += amount;
    }

    public void PurchaseUpgrade(int amount)
    {
        if (currentCoinCount >= amount)
        {
            currentCoinCount -= amount;
            currentUpgradeCount++;
        }
    }
}