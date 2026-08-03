using UnityEngine;

public class OnCircleColliderChecker : MonoBehaviour
{
    [Tooltip("Search radius on the XZ plane around this transform.")]
    [SerializeField] private float searchRadius = 5f;

    [Tooltip("How many random samples to try before giving up.")]
    [SerializeField] private int maxAttempts = 12;

    [Tooltip("Minimum clearance (sphere radius) required around the candidate position.")]
    [SerializeField] private float clearance = 0.5f;

    [Tooltip("Layers considered as obstacles when checking for a free position.")]
    [SerializeField] private LayerMask obstacleMask = ~0;

    public bool TryFindSafePosition(out Vector3 result)
    {
        return TryFindSafePosition(searchRadius, maxAttempts, clearance, obstacleMask, out result);
    }
    public bool TryFindSafePosition(float radius, int attempts, float requiredClearance, LayerMask mask, out Vector3 result)
    {
        Vector3 origin = transform.position;
        float y = origin.y;

        for (int i = 0; i < Mathf.Max(1, attempts); i++)
        {
            Vector2 circle = Random.insideUnitCircle * radius;
            Vector3 candidate = new Vector3(origin.x + circle.x, y+0.5f, origin.z + circle.y);

            // If there's no collider within requiredClearance at candidate, it's safe
            bool occupied = Physics.CheckSphere(candidate, requiredClearance, mask, QueryTriggerInteraction.Ignore);
            if (!occupied)
            {
                result = new Vector3(candidate.x, candidate.y - 0.5f, candidate.z);
                return true;
            }
        }

        result = origin;
        return false;
    }

    public Vector3 GetSafePositionOrOrigin()
    {
        if (TryFindSafePosition(out Vector3 pos))
            return pos;

        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
 
        // draw search circle on XZ plane
        Vector3 center = transform.position;
        const int seg = 36;
        Vector3 prev = center + new Vector3(searchRadius, 0f, 0f);
        for (int i = 1; i <= seg; i++)
        {
            float ang = (i / (float)seg) * Mathf.PI * 2f;
            Vector3 next = center + new Vector3(Mathf.Cos(ang) * searchRadius, 0f, Mathf.Sin(ang) * searchRadius);
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
}
