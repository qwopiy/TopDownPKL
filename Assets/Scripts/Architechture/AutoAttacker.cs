using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetFinder))]
[RequireComponent(typeof(UnitStatsManager))]
public class AutoAttacker : MonoBehaviour
{
    private TargetFinder targetFinder;
    private UnitStatsManager statsManager;

    private float attackRange;
    private float attackRate;
    private float nextAttackTime;

    [Header("Visual Feedback Placeholder (Graybox)")]
    [SerializeField] private float visualFeedbackDuration = 0.1f;
    private Vector3 originalScale;
    private bool isSqueezing = false;

    private void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
        statsManager = GetComponent<UnitStatsManager>();
        
        attackRate = statsManager.CurrentAttackRate; //nanti diganti jadi pake Event
        attackRange = statsManager.CurrentAttackRange; //nanti diganti jadi pake Event

        originalScale = transform.localScale;
    }

    private void Update()
    {
        Transform target = targetFinder.NearestTarget;

        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);
        attackRange = statsManager.CurrentAttackRange;

        if (distance <= attackRange)
        {
            RotateTowardsTarget(target);

            if (Time.time >= nextAttackTime)
            {
                Attack(target);
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f; 

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void Attack(Transform target) 
    {
        AttackBehaviorSO behavior = statsManager.CharacterData.attackBehavior;

        if (behavior != null)
        {
            behavior.ExecuteAttack(gameObject, target, statsManager.CurrentAttackDamage);

            if (!isSqueezing) StartCoroutine(AttackVisualFeedbackRoutine());
        }
    }

    private IEnumerator AttackVisualFeedbackRoutine()
    {
        isSqueezing = true;
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.9f, originalScale.z * 1.2f);

        yield return new WaitForSeconds(visualFeedbackDuration);

        transform.localScale = originalScale;
        isSqueezing = false;
    }
}