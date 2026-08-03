using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    private Transform[] waypoints;

    public Transform[] Waypoints => waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}