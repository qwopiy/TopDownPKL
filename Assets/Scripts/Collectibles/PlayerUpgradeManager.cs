using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
public class PlayerUpgradeManager : MonoBehaviour
{
    public static PlayerUpgradeManager Instance;

    public int currentCoinCount = 0;
    public int currentUpgradeCount = 0;
    public int baseUpgradeCost = 10; // Base cost for the first upgrade

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

        if (IsUpgradable())
        {
            TriggerUpgrade();
        }
    }

    public void TriggerUpgrade()
    {
        currentCoinCount -= GetUpgradeCost();

        // tampilin layar upgrade + event upgrade
        Debug.Log("Upgrade triggered! Current upgrade count: " + currentUpgradeCount);

        currentUpgradeCount++;
    }

    public bool IsUpgradable()
    {
        // Check if the player has enough coins to upgrade
        return currentCoinCount >= GetUpgradeCost();
    }

    private int GetUpgradeCost()
    {
        return baseUpgradeCost + (currentUpgradeCount * 2);
    }
}