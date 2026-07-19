using UnityEngine;

public class PlayerAnchorAssigner : MonoBehaviour
{
    [SerializeField] private TransformAnchorSO playerTransformAnchor;
    [SerializeField] private SquadAnchorSO squadAnchorSO;

    private void OnEnable()
    {
        if (playerTransformAnchor != null)
        {
            playerTransformAnchor.value = this.transform;
        }
        if (squadAnchorSO != null)
        {
            squadAnchorSO.Add(this.gameObject);
        }
    }

    private void OnDisable()
    {
        if (playerTransformAnchor != null && playerTransformAnchor.value == this.transform)
        {
            playerTransformAnchor.value = null;
        }
        if (squadAnchorSO != null)
        {
            squadAnchorSO.Remove(this.gameObject);
        }
    }
}