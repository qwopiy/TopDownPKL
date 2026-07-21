using System.Collections;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float scanInterval = 0.2f; 

    private Transform nearestTarget;
    public Transform NearestTarget => nearestTarget;
    private void Start()
    {
        StartCoroutine(ScanForTargetsRoutine());
    }

    private IEnumerator ScanForTargetsRoutine()
    {
        while (true)
        {
            ScanForNearestTarget();
            yield return new WaitForSeconds(scanInterval);
        }
    }

    private void ScanForNearestTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestTransform = null;

        foreach (var col in hitColliders)
        {
            if (col.GetComponent<UnitStatsManager>() == null) continue;

            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTransform = col.transform;
            }
        }

        nearestTarget = closestTransform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}