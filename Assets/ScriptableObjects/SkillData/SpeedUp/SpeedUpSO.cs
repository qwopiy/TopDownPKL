using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "ScriptableObjects/Skills/Speed Up")]
public class SpeedUpSO : SkillDataSO
{
    public UpgradeDataSO upgradeData;
    public float duration = 5f;
    public override void Activate(GameObject caster)
    {
        caster.GetComponent<UnitStatsManager>()?.ApplyTemporaryUpgrade(upgradeData, duration);
    }
}
