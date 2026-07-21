using UnityEngine;

[RequireComponent(typeof(PlayerDetector))]
[RequireComponent(typeof(EnemyStatsManager))]
public class EnemyAttacker : MonoBehaviour
{
    private PlayerDetector detector;
    private EnemyStatsManager stats;

    private float nextAttackTime;

    private void Awake()
    {
        detector = GetComponent<PlayerDetector>();
        stats = GetComponent<EnemyStatsManager>();
    }

    private void Update()
    {
        Transform target = detector.DetectedPlayer;

        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stats.CurrentAttackRange)
            return;

        RotateTowardsTarget(target);

        if (Time.time >= nextAttackTime)
        {
            Attack(target);
            nextAttackTime = Time.time + stats.CurrentAttackRate;
        }
    }
        
    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void Attack(Transform target)
    {
        AttackBehaviorSO behavior = stats.EnemyData.attackBehavior;

        if (behavior == null)
            return;

        behavior.ExecuteAttack(
            gameObject,
            target,
            stats.CurrentAttackDamage);
    }
}