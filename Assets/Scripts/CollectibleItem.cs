using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public float amount = 1f;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollectibleItem: OnTriggerEnter called with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        PlayerManager.Instance.currentGold += amount;
        Destroy(gameObject);
    }
}
