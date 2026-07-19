using UnityEngine;

public enum ModifierType { Flat, Percent }
public enum StatType { MaxHealth, MovementSpeed, AttackDamage, AttackSpeed }

[System.Serializable]
public class StatsModifier 
{
    public StatType statType;
    public ModifierType modType;
    public float value;
}
