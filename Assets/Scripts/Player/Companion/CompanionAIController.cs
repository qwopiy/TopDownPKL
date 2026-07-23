using UnityEngine;

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

    private CharacterController controller;
    private UnitStatsManager statsManager;
    private TargetFinder targetFinder;

    private float currentAttackRange;
    private float currentRotationSpeed;

    public float CurrentSpeed => controller != null ? controller.velocity.magnitude : 0f;

    public bool IsMoving => CurrentSpeed > 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        statsManager = GetComponent<UnitStatsManager>();
        targetFinder = GetComponent<TargetFinder>(); 

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
        if (distanceToPlayer > maximumDistanceFromPlayer)
        {
            targetToFollow = playerTransformAnchor.value;
            targetStopDistance = stopDistanceBeforePlayer;
        }
        else if (targetFinder != null && targetFinder.NearestTarget != null )
        {
            targetToFollow = targetFinder.NearestTarget;
            // Berhenti sedikit sebelum jangkauan maksimum serangan agar serangan masuk
            targetStopDistance = currentAttackRange - 0.5f;
        }
        else 
        {
            targetToFollow = playerTransformAnchor.value;
            targetStopDistance = stopDistanceBeforePlayer;
        }

        if (targetToFollow == null) return;

        Vector3 direction = targetToFollow.position - transform.position;
        direction.y = 0f; 
        float distance = direction.magnitude;

        if (distance > targetStopDistance)
        {
            direction.Normalize();
            float speed = statsManager.CurrentMovementSpeed;
            controller.Move(direction * speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);
        }
    }

    public void Recruit()
    {
        currentState = CompanionState.Following;
    }
}