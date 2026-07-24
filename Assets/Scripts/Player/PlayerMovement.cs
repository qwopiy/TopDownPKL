using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMovementProvider
{
    [Header("references")]
    [SerializeField] private InputReader inputReader;
    private UnitStatsManager unitStatsManager;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private CharacterController controller;
    private Transform cameraTransform;

    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    public float CurrentSpeed => controller != null ? controller.velocity.magnitude : 0f;
    public bool IsMoving => CurrentSpeed > 0.1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        unitStatsManager = GetComponent<UnitStatsManager>();

        cameraTransform = Camera.main.transform;

        inputReader.MovementChanged += OnMove;
    }

    public void OnMove(Vector2 _value)
    {
        inputVector = _value;
    }

    void Update()
    {
        moveSpeed = unitStatsManager.CurrentMovementSpeed;
        rotationSpeed = unitStatsManager.CurrentRotationSpeed;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = (right * inputVector.x) + (forward * inputVector.y);

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
