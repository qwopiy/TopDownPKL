using UnityEngine;

public class PlayerAnchorAssigner : MonoBehaviour
{
    [SerializeField] private TransformAnchorSO playerTransformAnchor;

    private void OnEnable()
    {
        if (playerTransformAnchor != null)
        {
            playerTransformAnchor.value = this.transform;
        }
    }

    private void OnDisable()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.value == this.transform)
        {
            playerTransformAnchor.value = null;
        }
    }
}