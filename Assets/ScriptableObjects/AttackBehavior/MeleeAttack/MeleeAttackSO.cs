using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeAttack", menuName = "ScriptableObjects/Attack Behaviors/Melee")]
public class MeleeAttackSO : AttackBehaviorSO
{
    private AutoAttacker autoAttacker;

    [SerializeField] private string animationName;
    private int animHash;
    public override void ExecuteAttack(GameObject attacker, Transform target, float damage)
    {
        animHash = Animator.StringToHash(animationName);

        autoAttacker = attacker.GetComponent<AutoAttacker>();

        UnitStatsManager enemyStats = target.GetComponent<UnitStatsManager>();
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(damage);
            autoAttacker.OnAnimationPlayed(animHash);

            Debug.Log($"{attacker.name} memukul {target.name} dari jarak dekat!");
        }
    }
}