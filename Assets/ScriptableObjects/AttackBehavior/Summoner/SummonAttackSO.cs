using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSummonAttack", menuName = "ScriptableObjects/Attack Behaviors/Summon")]
public class SummonAttackSO : AttackBehaviorSO
{
    [SerializeField] private GameObject summonGO;

    private AutoAttacker autoAttacker;
    OnCircleColliderChecker onCircleColliderChecker;

    [SerializeField] private string animationName;
    private int animHash;
    public override void ExecuteAttack(GameObject attacker, Transform target, float damage)
    {
        animHash = Animator.StringToHash(animationName);

        autoAttacker = attacker.GetComponent<AutoAttacker>();
        autoAttacker.OnAnimationPlayed(animHash);

        onCircleColliderChecker = attacker.GetComponent<OnCircleColliderChecker>();
        GameObject summon_1 = Instantiate(summonGO, onCircleColliderChecker.GetSafePositionOrOrigin(), Quaternion.identity);
        summon_1.GetComponent<RecruitableCompanion>().AddToSquad();
        //Debug.Log(summon_1.GetComponent<RecruitableCompanion>());
        GameObject summon_2 = Instantiate(summonGO, onCircleColliderChecker.GetSafePositionOrOrigin(), Quaternion.identity);
        summon_2.GetComponent<RecruitableCompanion>().AddToSquad();
    }
}
