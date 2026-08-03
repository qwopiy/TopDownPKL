using System;
using UnityEngine;
public class PlayerUpgradeManager : MonoBehaviour
{
    public static PlayerUpgradeManager Instance;
    public event Action<int> CoinCollected;

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
        CoinCollected?.Invoke(currentCoinCount);
    }

    public void AddCoins(int amount)
    {
        currentCoinCount += amount;
        CoinCollected?.Invoke(currentCoinCount);
    }

    public bool TryPurchaseUpgrade(int amount)
    {
        if (currentCoinCount >= amount)
        {
            PurchaseUpgrade(amount);
            return true;
        }
        return false;
    }

    public void PurchaseUpgrade(int amount) 
    {
        currentCoinCount -= amount;
        currentUpgradeCount++;
        CoinCollected?.Invoke(currentCoinCount);
    }
}