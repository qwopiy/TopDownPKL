using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/Enemy Data")]
public class EnemyDataSO : CharacterDataSO
{
    [Header("Base Info")]
    public string enemyName;

    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float movementSpeed = 3.5f;
    public float rotationSpeed = 720f;

    [Header("Navigation")]
    public float acceleration = 8f;
    public float stoppingDistance = 1f;
    public bool autoBraking = true;

    [Header("Combat Stats")]
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackRate = 1f;

    [Header("Detection")]
    public float detectionRadius = 6f;

    [Header("Attack Behavior")]
    public AttackBehaviorSO attackBehavior;

    [Header("Reward")]
    public int expReward = 10;
    public int goldReward = 5;
}