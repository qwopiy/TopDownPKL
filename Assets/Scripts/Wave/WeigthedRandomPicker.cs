using System.Collections.Generic;
using UnityEngine;

public static class WeightedRandomPicker
{
    public static List<GameObject> GetRandomTier(
        EnemyTierSO enemyTierSO,
        WaveDifficulty difficulty)
    {
        int totalWeight =
            difficulty.normalWeight +
            difficulty.tier2Weight +
            difficulty.tier3Weight +
            difficulty.tier4Weight;

        int random = Random.Range(0, totalWeight);

        if (random < difficulty.normalWeight)
            return enemyTierSO.normalEnemyPrefab;

        random -= difficulty.normalWeight;

        if (random < difficulty.tier2Weight)
            return enemyTierSO.tier2EnemyPrefab;

        random -= difficulty.tier2Weight;

        if (random < difficulty.tier3Weight)
            return enemyTierSO.tier3EnemyPrefab;

        return enemyTierSO.tier4EnemyPrefab;
    }
}