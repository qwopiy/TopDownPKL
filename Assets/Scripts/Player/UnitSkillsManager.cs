using System.Collections.Generic;
using UnityEngine;

public class UnitSkillManager : MonoBehaviour
{
    [Header("Equipped Skills")]
    [SerializeField] private List<SkillDataSO> equippedSkills = new List<SkillDataSO>();

    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();

    private void Update()
    {
        List<string> keys = new List<string>(cooldownTimers.Keys);
        foreach (var key in keys)
        {
            if (cooldownTimers[key] > 0)
            {
                cooldownTimers[key] -= Time.deltaTime;
            }
        }
    }
    public void CastSkill(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= equippedSkills.Count) return;

        SkillDataSO skill = equippedSkills[skillIndex];

        if (IsSkillReady(skill))
        {
            skill.Activate(gameObject);

            cooldownTimers[skill.skillName] = skill.cooldown;
        }
        else
        {
            Debug.Log($"Skill {skill.skillName} masih cooldown! Sisa waktu: {cooldownTimers[skill.skillName]:F1}s");
        }
    }

    public bool IsSkillReady(SkillDataSO skill)
    {
        if (!cooldownTimers.ContainsKey(skill.skillName)) return true;
        return cooldownTimers[skill.skillName] <= 0;
    }

    public void AddSkill(SkillDataSO newSkill)
    {
        if (!equippedSkills.Contains(newSkill))
        {
            equippedSkills.Add(newSkill);
        }
    }
}