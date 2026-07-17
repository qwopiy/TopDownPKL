using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeAttack", menuName = "ScriptableObjects/Attack Behaviors/Melee")]
public class MeleeAttackSO : AttackBehaviorSO
{
    public override void ExecuteAttack(GameObject attacker, Transform target, float damage)
    {
        // Logika Melee: Langsung kurangi darah target
        UnitStatsManager enemyStats = target.GetComponent<UnitStatsManager>();
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(damage);
            Debug.Log($"{attacker.name} memukul {target.name} dari jarak dekat!");
        }
    }
}