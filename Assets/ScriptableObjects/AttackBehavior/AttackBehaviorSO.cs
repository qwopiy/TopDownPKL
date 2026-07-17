using UnityEngine;

public abstract class AttackBehaviorSO : ScriptableObject
{
    public abstract void ExecuteAttack(GameObject attacker, Transform target, float damage);
}