using System.Collections.Generic;
using UnityEngine;

public class UpgradeRandomizer 
{
    public List<UpgradeDataSO> allUpgrades;
    public List<CompanionDataSO> allCompanions;

    public UpgradeDataSO GetRandomUpgrade()
    {
        if (allUpgrades.Count == 0)
            return null;
        int randomIndex = Random.Range(0, allUpgrades.Count);
        return allUpgrades[randomIndex];
    }
    public CompanionDataSO GetRandomCompanion()
    {
        if (allCompanions.Count == 0)
            return null;

        int randomIndex = Random.Range(0, allCompanions.Count);
        return allCompanions[randomIndex];
    }


    public List<UpgradeDataSO> GetRandomUpgrades(int count)
    {
        List<UpgradeDataSO> randomUpgrades = new List<UpgradeDataSO>();
        List<UpgradeDataSO> availableUpgrades = new List<UpgradeDataSO>(allUpgrades);
        for (int i = 0; i < count && availableUpgrades.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            randomUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }
        return randomUpgrades;
    }

    public List<CompanionDataSO> GetRandomCompanions(int count)
    {
        List<CompanionDataSO> randomCompanions = new List<CompanionDataSO>();
        List<CompanionDataSO> availableCompanions = new List<CompanionDataSO>(allCompanions);
        for (int i = 0; i < count && availableCompanions.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableCompanions.Count);
            randomCompanions.Add(availableCompanions[randomIndex]);
            availableCompanions.RemoveAt(randomIndex);
        }
        return randomCompanions;
    }
}