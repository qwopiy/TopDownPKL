using System.Collections;
using UnityEngine;
public class ResourceDropper : MonoBehaviour
{
    public GameObject resourcePrefab;
    private void OnDestroy()
    {
        Instantiate(resourcePrefab);
    }
}