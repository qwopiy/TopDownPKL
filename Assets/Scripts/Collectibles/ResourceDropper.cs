using UnityEngine;
public class ResourceDropper : MonoBehaviour
{
    public GameObject resourcePrefab;
    private void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Instantiate(resourcePrefab, transform.position, Quaternion.identity);
    }
}