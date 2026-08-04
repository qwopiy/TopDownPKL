using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatsManager : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    [SerializeField] private GameEventSO unitDied;

    private List<StatsModifier> activeModifiers = new List<StatsModifier>();

    public float currentHealth;
    public float currentMovementSpeed;
    public float currentAttackDamage;
    public float currentRotationSpeed;
    public float currentAttackRate;
    public float currentAttackRange;

    public float CurrentMovementSpeed => CalculateStat(StatType.MovementSpeed, currentMovementSpeed);
    public float CurrentAttackDamage => CalculateStat(StatType.AttackDamage, currentAttackDamage);
    public float CurrentAttackRate => CalculateStat(StatType.AttackSpeed, currentAttackRate);

    public float CurrentAttackRange => currentAttackRange;

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
            currentAttackRate = characterData.attackRate;
            currentAttackRange = characterData.attackRange;
        }
        else
        {
            Debug.LogWarning($"CharacterDataSO belum dipasang di GameObject: {gameObject.name}");
        }
    }

    public void ApplyUpgrade(UpgradeDataSO upgrade)
    {
        foreach (var mod in upgrade.modifiers)
        {
            activeModifiers.Add(mod);
        }
    }

    public void ApplyTemporaryUpgrade(UpgradeDataSO upgrade, float duration)
    {
        ApplyUpgrade(upgrade);
        StartCoroutine(RemoveUpgradeAfterDuration(upgrade, duration));
    }

    public void RemoveUpgrade(UpgradeDataSO upgrade)
    {
        foreach (var mod in upgrade.modifiers)
        {
            activeModifiers.Remove(mod);
        }
    }
    private IEnumerator RemoveUpgradeAfterDuration(UpgradeDataSO upgrade, float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveUpgrade(upgrade);
    }

    private float CalculateStat(StatType type, float baseValue)
    {
        float finalValue = baseValue;
        float percentSum = 0;

        foreach (var mod in activeModifiers)
        {
            if (mod.statType != type) continue;

            if (mod.modType == ModifierType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.modType == ModifierType.Percent)
            {
                percentSum += mod.value; 
            }
        }

        finalValue *= (1f + percentSum);
        return finalValue;
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

        PatrolController patrol = GetComponent<PatrolController>();

        if (patrol != null)
        {
            patrol.Spawner.NotifyEnemyDead(gameObject);
        }


        Debug.Log($"{gameObject.name} telah dikalahkan!");
        unitDied?.Raise();
        Destroy(gameObject); 
    }
}