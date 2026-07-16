using UnityEngine;

public class IsometricCameraMovement : MonoBehaviour
{
    private Transform target; 

    [SerializeField] private Vector3 offset = new Vector3(-10, 10, -10); 
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
