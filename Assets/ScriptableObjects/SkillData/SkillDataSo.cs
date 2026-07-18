using UnityEngine;

public abstract class SkillDataSO : ScriptableObject
{
    [Header("Skill Identity")]
    public string skillName;
    [TextArea] public string description;
    public Sprite skillIcon;

    [Header("Skill Settings")]
    public float cooldown;
    public float energyCost; // Opsional

    public abstract void Activate(GameObject caster);
}