using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius = 6f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float scanInterval = 0.2f;

    public Transform DetectedPlayer { get; private set; }

    private void Start()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        while (true)
        {
            ScanPlayer();
            yield return new WaitForSeconds(scanInterval);
        }
    }

    private void ScanPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectionRadius,
            playerLayer);

        if (hits.Length > 0)
        {
            // Asumsi hanya ada satu player
            DetectedPlayer = hits[0].transform;
        }
        else
        {
            DetectedPlayer = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}