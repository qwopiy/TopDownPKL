using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private EnemyMovement movement;
    private TargetFinder detector;

    private Transform tower;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        detector = GetComponent<TargetFinder>();

        GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");

        if (towerObject != null)
            tower = towerObject.transform;
    }

    private void Update()
    {
        if (detector.NearestTarget != null)
        {
            movement.MoveTo(detector.NearestTarget.position);
        }
        else if (tower != null)
        {
            movement.MoveTo(tower.position);
        }
    }

    private void OnDestroy()
    {
        WaveManager.Instance.EnemyDied();
    }
}