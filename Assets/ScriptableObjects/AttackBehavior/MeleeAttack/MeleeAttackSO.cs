using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeAttack", menuName = "ScriptableObjects/Attack Behaviors/Melee")]
public class MeleeAttackSO : AttackBehaviorSO
{
    private AutoAttacker autoAttacker;

    [SerializeField] private string animationName;
    private int animHash;

    private Transform target;
    private float damage;
    public override void ExecuteAttack(GameObject attacker, Transform _target, float _damage)
    {
        animHash = Animator.StringToHash(animationName);

        autoAttacker = attacker.GetComponent<AutoAttacker>();
        autoAttacker.OnAnimationPlayed(animHash);
    }
}