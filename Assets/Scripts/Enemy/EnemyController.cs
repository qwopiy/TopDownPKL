using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(PlayerDetector))]
public class EnemyController : MonoBehaviour
{
    private EnemyMovement movement;
    private PlayerDetector detector;

    private Transform tower;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        detector = GetComponent<PlayerDetector>();

        GameObject towerObject = GameObject.FindGameObjectWithTag("Tower");

        if (towerObject != null)
            tower = towerObject.transform;
    }

    private void Update()
    {
        if (detector.DetectedPlayer != null)
        {
            movement.MoveTo(detector.DetectedPlayer.position);
        }
        else if (tower != null)
        {
            movement.MoveTo(tower.position);
        }
    }
}