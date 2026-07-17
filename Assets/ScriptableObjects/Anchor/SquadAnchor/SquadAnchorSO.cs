using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquadAnchor", menuName = "ScriptableObjects/Anchor/SquadAnchor")]
public class SquadAnchorSO : ScriptableObject
{
    public List<GameObject> members = new List<GameObject>();

    public void Add(GameObject obj)
    {
        if (!members.Contains(obj))
        {
            members.Add(obj);
        }
    }

    public void Remove(GameObject obj)
    {
        if (members.Contains(obj))
        {
            members.Remove(obj);
        }
    }

    private void OnDisable()
    {
        members.Clear();
    }
}
