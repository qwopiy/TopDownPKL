using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillManager : MonoBehaviour
{
    [Header("Passive Skills Equipped")]
    [SerializeField] private List<SkillDataSO> passiveSkills = new List<SkillDataSO>();

    private Dictionary<string, float> passiveTimers = new Dictionary<string, float>();

    private void Start()
    {
        foreach (var skill in passiveSkills)
        {
            if (skill != null)
            {
                passiveTimers[skill.skillName] = 0f;
            }
        }
    }

    private void Update()
    {
        foreach (var skill in passiveSkills)
        {
            if (skill == null) continue;

            if (!passiveTimers.ContainsKey(skill.skillName))
            {
                passiveTimers[skill.skillName] = skill.cooldown;
            }

            if (passiveTimers[skill.skillName] > 0)
            {
                passiveTimers[skill.skillName] -= Time.deltaTime;
            }
            else
            {
                ExecutePassiveSkill(skill);
            }
        }
    }

    private void ExecutePassiveSkill(SkillDataSO skill)
    {
        skill.Activate(gameObject);

        passiveTimers[skill.skillName] = skill.cooldown;
    }

    public void AddPassiveSkill(SkillDataSO newPassive)
    {
        if (!passiveSkills.Contains(newPassive))
        {
            passiveSkills.Add(newPassive);
            passiveTimers[newPassive.skillName] = newPassive.cooldown;
        }
    }
}
