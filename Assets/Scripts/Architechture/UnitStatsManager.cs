using UnityEngine;

public class UnitStatsManager : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    [SerializeField] private GameEventSO unitDied;

    [SerializeField] private float currentHealth;
    private float currentMovementSpeed;
    private float currentAttackDamage;
    private float currentRotationSpeed;

    public float CurrentMovementSpeed => currentMovementSpeed;
    public float CurrentAttackDamage => currentAttackDamage;

    public float CurrentRotationSpeed => currentRotationSpeed;
    public CharacterDataSO CharacterData => characterData;

    private void Awake()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        if (characterData != null)
        {
            currentHealth = characterData.maxHealth;
            currentMovementSpeed = characterData.movementSpeed;
            currentAttackDamage = characterData.attackDamage;
            currentRotationSpeed = characterData.rotationSpeed;
        }
        else
        {
            Debug.LogWarning($"CharacterDataSO belum dipasang di GameObject: {gameObject.name}");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} terkena damage {amount}. Sisa HP: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} telah dikalahkan!");
        unitDied?.Raise();
        gameObject.SetActive(false); 
    }
}