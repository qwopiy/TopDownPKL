using UnityEngine;
[CreateAssetMenu(fileName = "NewCompanion", menuName = "ScriptableObjects/Companion Data")]
public class CompanionDataSO : UpgradeDataSO
{
    [Tooltip("Prefab of the companion to be spawned, WARNING!! modifier gadipake di SO ini")]
    public GameObject companionPrefab;
    public Vector3 spawnCoordinates;

    public void SpawnCompanion()
    {
        if (companionPrefab != null)
        {
            GameObject newCompanion = Instantiate(companionPrefab, spawnCoordinates, Quaternion.identity);
            // Additional logic for the companion can be added here
        }
        else
        {
            Debug.LogWarning("Companion prefab is not assigned in CompanionDataSO.");
        }
    }
}