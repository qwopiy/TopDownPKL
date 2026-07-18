using UnityEngine;

[CreateAssetMenu(fileName = "MeteorTemp", menuName = "ScriptableObjects/Skills/Meteor Temporary")]
public class MeteorSkillSO : SkillDataSO
{
    public GameObject meteorPrefab;

    public override void Activate(GameObject caster) { 
        Instantiate(meteorPrefab, new Vector3 (caster.transform.position.x, caster.transform.position.y + 5, caster.transform.position.z), caster.transform.rotation);
    }
}
