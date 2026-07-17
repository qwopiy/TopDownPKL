using UnityEngine;

[CreateAssetMenu(fileName = "NewTransformAnchor", menuName = "ScriptableObjects/Anchor/Transform Anchor")]
public class TransformAnchorSO : ScriptableObject
{
    [System.NonSerialized]
    public Transform value;

    private void OnDisable()
    {
        value = null;
    }
}