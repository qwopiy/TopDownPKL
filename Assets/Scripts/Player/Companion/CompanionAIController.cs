using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(UnitStatsManager))]
public class CompanionAIController : MonoBehaviour, IMovementProvider
{
    public enum CompanionState { Idle, Following }
    [Header("State")]
    [SerializeField] private CompanionState currentState = CompanionState.Idle;

    [Header("References (SO)")]
    [SerializeField] private TransformAnchorSO playerTransformAnchor;

    [Header("Movement Tuning")]
    [SerializeField] private float stopDistanceBeforePlayer = 2f; // Jarak aman dari Player
    [SerializeField] private float maximumDistanceFromPlayer = 5f; // Jarak maksimum dari Player sebelum kembali mengikuti
    [SerializeField] private float gravity = -12;
    private float velocityY;

    private CharacterController controller;
    private UnitStatsManager statsManager;
    private TargetFinder targetFinder;
    private TargetFinder playerTargetFinder;

    private float currentAttackRange;
    private float currentRotationSpeed;

    public float CurrentSpeed => controller != null ? controller.velocity.magnitude : 0f;

    public bool IsMoving => CurrentSpeed > 0.001f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        statsManager = GetComponent<UnitStatsManager>();
        targetFinder = GetComponent<TargetFinder>();

        playerTargetFinder = playerTransformAnchor.value.GetComponent<TargetFinder>();

        currentAttackRange = statsManager.CurrentAttackRange; //nanti ditambah event atau dimasukkan ke update
        currentRotationSpeed = statsManager.CurrentRotationSpeed;
    }

    private void Update()
    {
        if (currentState == CompanionState.Idle) return;

        Transform targetToFollow = null;
        float targetStopDistance = stopDistanceBeforePlayer;

        if (playerTransformAnchor == null || playerTransformAnchor.value == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransformAnchor.value.position);

        //Target Prioritization: 1. Player, 2. Nearest Target, 3. Player's Target
        if (distanceToPlayer > maximumDistanceFromPlayer)
        {
            targetToFollow = playerTransformAnchor.value;
            targetStopDistance = stopDistanceBeforePlayer;
        }
        else if (targetFinder != null && targetFinder.NearestTarget != null)
        {
            targetToFollow = targetFinder.NearestTarget;
            targetStopDistance = currentAttackRange - 0.5f;
        }
        
        else if (playerTargetFinder.NearestTarget != null)
        {
            targetToFollow = playerTargetFinder.NearestTarget;
            targetStopDistance = currentAttackRange - 0.5f;
        }
        else 
        {
            targetToFollow = playerTransformAnchor.value;
            targetStopDistance = stopDistanceBeforePlayer;
        }

        if (targetToFollow == null) return;

        //Speed Adjustment: If the companion is too far from the player, it will move faster to catch up.
        float speed;
        if (distanceToPlayer > maximumDistanceFromPlayer)
        {
            speed = statsManager.CurrentMovementSpeed * 1.5f;
        }
        else
        {
            speed = statsManager.CurrentMovementSpeed;
        }

        Vector3 direction = targetToFollow.position - transform.position;
        direction.y = 0f; 
        float distance = direction.magnitude;

        if (distance > targetStopDistance)
        {
            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);
        }
        else
        {
            direction = Vector3.zero;
        }

        Vector3 velocity = direction * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }

    public void Recruit()
    {
        currentState = CompanionState.Following;
    }
}