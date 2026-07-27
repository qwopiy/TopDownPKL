using UnityEngine;

public static class WaveDifficultyCalculator
{
    public static WaveDifficulty GetDifficulty(int wave)
    {
        WaveDifficulty difficulty = new WaveDifficulty();

        difficulty.normalWeight = Mathf.Max(20, 100 - wave * 2);

        difficulty.tier2Weight = Mathf.Clamp((wave - 5) * 2, 0, 40);

        difficulty.tier3Weight = Mathf.Clamp((wave - 15) * 2, 0, 30);

        difficulty.tier4Weight = Mathf.Clamp((wave - 30), 0, 10);

        return difficulty;
    }
}