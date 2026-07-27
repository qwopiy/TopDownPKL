using System.Collections.Generic;
using UnityEngine;

public class UpgradeRandomizerManager : MonoBehaviour
{
    public static UpgradeRandomizerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Upgrade Settings")]
    public List<UpgradeDataSO> upgradeTemplates;
    public List<float> upgradeChances;

    [Header("Flat Modifier Settings")]
    public List<float> flatModifier;
    public List<float> flatChances;

    [Header("Percentage Modifier Settings")]
    public List<float> percentageModifier;
    public List<float> percentageChances;

    [Header("Companion Settings")]
    public List<CompanionDataSO> companionTemplates;
    public List<float> companionChances;

    public UpgradeDataSO GetRandomUpgrade()
    {
        if (upgradeTemplates.Count == 0)
            return null;

        return RandomUpgradeIncrease(upgradeTemplates[GetRandomUpgradeIndex(upgradeChances)]);
    }
    public CompanionDataSO GetRandomCompanion()
    {
        if (companionTemplates.Count == 0)
            return null;

        int randomIndex = Random.Range(0, companionTemplates.Count);
        return companionTemplates[GetRandomUpgradeIndex(companionChances)];
    }

    private UpgradeDataSO RandomUpgradeIncrease(UpgradeDataSO originalUpgradeSO)
    {
        if (originalUpgradeSO == null || originalUpgradeSO.modifiers == null)
            return null;

        if (flatModifier == null || percentageModifier == null)
            return null;

        float upgradeModifier = 0f;
        if (originalUpgradeSO.modifiers[0].modType == ModifierType.Flat)
        {
            upgradeModifier = flatModifier[GetRandomUpgradeIndex(flatChances)];
        }
        else if (originalUpgradeSO.modifiers[0].modType == ModifierType.Percent)
        {
            upgradeModifier = percentageModifier[GetRandomUpgradeIndex(percentageChances)];
        }
        else
        {
            return null;
        }

        // 2. Instantiate a brand-new ScriptableObject instance in memory
        UpgradeDataSO newUpgradeSO = Instantiate(originalUpgradeSO);

        // 3. Deep-copy the modifiers list so we don't modify original references
        newUpgradeSO.modifiers = new List<StatsModifier>();

        for (int i = 0; i < originalUpgradeSO.modifiers.Count; i++)
        {
            var origMod = originalUpgradeSO.modifiers[i];

            var newMod = new StatsModifier();
            {
                newMod.modType = origMod.modType;
                newMod.value = origMod.value + upgradeModifier;
                // Copy any other fields here
            }
            ;

            newUpgradeSO.modifiers.Add(newMod);
        }
        newUpgradeSO.description = originalUpgradeSO.description + $" {originalUpgradeSO.modifiers[0].value} {originalUpgradeSO.modifiers[0].modType} (Increased by {upgradeModifier})";

        return newUpgradeSO;
    }

    public int GetRandomUpgradeIndex(List<float> chances)
    {
        if (chances == null || chances.Count == 0)
            return -1; // Or return 0 depending on your error fallback design

        // 1. Calculate total sum of all weights
        float totalWeight = 0f;
        for (int i = 0; i < chances.Count; i++)
        {
            totalWeight += chances[i];
        }

        if (totalWeight <= 0f)
            return 0;

        // 2. Roll a random number between 0 (inclusive) and totalWeight (exclusive)
        float roll = Random.Range(0f, totalWeight);

        // 3. Step through items using index 'i' directly
        float cumulativeWeight = 0f;
        for (int i = 0; i < chances.Count; i++)
        {
            cumulativeWeight += chances[i];
            if (roll < cumulativeWeight)
            {
                return i; // Directly returns the correct index, even with equal values!
            }
        }

        // Fallback case due to floating point rounding precision
        return chances.Count - 1;
    }
}