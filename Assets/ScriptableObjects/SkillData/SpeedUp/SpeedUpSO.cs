using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "ScriptableObjects/Skills/Speed Up")]
public class SpeedUpSO : SkillDataSO
{
    public SquadAnchorSO squadAnchor;
    public UpgradeDataSO upgradeData;
    public float duration = 5f;
    public override void Activate(GameObject caster)
    {
        squadAnchor.ApplyTemporaryUpgrade(upgradeData, duration);
    }
}
