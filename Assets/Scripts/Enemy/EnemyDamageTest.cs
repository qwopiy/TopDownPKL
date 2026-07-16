using UnityEngine;

public class EnemyDamageTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnitStatsManager playerStats = other.GetComponent<UnitStatsManager>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(100f); 
            }
        }
    }
}
