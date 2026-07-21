using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;

    private float currentHealth;

    public EnemyDataSO EnemyData => enemyData;

    public float CurrentHealth => currentHealth;

    public float CurrentMovementSpeed => enemyData.movementSpeed;
    public float CurrentRotationSpeed => enemyData.rotationSpeed;

    public float CurrentAcceleration => enemyData.acceleration;
    public float CurrentStoppingDistance => enemyData.stoppingDistance;
    public bool CurrentAutoBraking => enemyData.autoBraking;

    public float CurrentAttackDamage => enemyData.attackDamage;
    public float CurrentAttackRange => enemyData.attackRange;
    public float CurrentAttackRate => enemyData.attackRate;

    public float CurrentDetectionRadius => enemyData.detectionRadius;

    private void Awake()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        if (enemyData == null)
        {
            Debug.LogError($"{name} belum memiliki EnemyDataSO.");
            return;
        }

        currentHealth = enemyData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}