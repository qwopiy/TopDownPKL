using System.Collections;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("References")]
    public TransformAnchorSO playerTransformAnchor;

    [Header("Collectible Settings")]
    public float magnetSpeed = 5f;
    public int amount = 1;

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
        PlayerUpgradeManager.Instance.AddCoins(amount);
        Destroy(gameObject);
    }

    public IEnumerator MoveToPlayer()
    {
        while (Vector3.Distance(transform.position, playerTransformAnchor.value.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransformAnchor.value.position, magnetSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
