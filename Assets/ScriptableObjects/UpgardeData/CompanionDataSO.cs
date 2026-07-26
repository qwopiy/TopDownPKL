using UnityEngine;
[CreateAssetMenu(fileName = "NewCompanion", menuName = "ScriptableObjects/Companion Data")]
public class CompanionDataSO : UpgradeDataSO
{
    [Tooltip("Prefab of the companion to be spawned, WARNING!! modifier gadipake di SO ini")]
    public GameObject companionPrefab;
}