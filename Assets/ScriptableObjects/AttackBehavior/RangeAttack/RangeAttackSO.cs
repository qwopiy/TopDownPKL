using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewRangeAttack", menuName = "ScriptableObjects/Attack Behaviors/Range")]
public class RangeAttackSO : AttackBehaviorSO
{
    [SerializeField] private GameObject projectilePrefabPlaceholder; 
    [SerializeField] private float projectileSpeed = 15f;

    [SerializeField] private string animationName;
    private int animHash;

    private AutoAttacker autoAttacker;

    public override void ExecuteAttack(GameObject attacker, Transform target, float damage)
    {
        Debug.Log($"{attacker.name} menembakkan proyektil ke {target.name}!");
        animHash = Animator.StringToHash(animationName);

        autoAttacker = attacker.GetComponent<AutoAttacker>();
        autoAttacker.OnAnimationPlayed(animHash);

        Vector3 direction = (target.position - attacker.transform.position).normalized;
        direction.y = 0f;

        GameObject bulletGO = Instantiate(projectilePrefabPlaceholder, new Vector3 (attacker.transform.position.x, attacker.transform.position.y, attacker.transform.position.z), attacker.transform.rotation);
        BulletController bulletController = bulletGO.GetComponent<BulletController>();
        if (bulletController != null) { 
            bulletController.AddBulletData(damage, projectileSpeed, direction);
        }
    }
}
