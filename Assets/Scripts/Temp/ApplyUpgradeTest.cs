using System;
using UnityEngine;

public class ApplyUpgradeTest : MonoBehaviour
{
    [SerializeField] private UpgradeDataSO upgradeData;
    [SerializeField] private SquadAnchorSO squadAnchor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //squadAnchor.ApplyUpgrade(upgradeData);
            other.GetComponent<ActiveSkillManager>()?.CastSkill(0);
        }
    }
}
