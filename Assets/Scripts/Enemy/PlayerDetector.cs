using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsManager))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float scanInterval = 0.2f;

    private EnemyStatsManager stats;

    public Transform DetectedPlayer { get; private set; }

    private void Awake()
    {
        stats = GetComponent<EnemyStatsManager>();
    }

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
            stats.CurrentDetectionRadius,
            playerLayer);

        DetectedPlayer = hits.Length > 0 ? hits[0].transform : null;
    }

    private void OnDrawGizmosSelected()
    {
        if (stats == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.CurrentDetectionRadius);
    }
}