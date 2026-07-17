using System;
using UnityEngine;
using UnityEngine.AI;   

public class EnemyControler : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent Agent;

    private Vector3 lastTargetPos;

    void Update()
    {
        if (Vector3.Distance(lastTargetPos, Target.position) > 1f)
        {
            lastTargetPos = Target.position;
            Agent.SetDestination(Target.position);
            print(lastTargetPos);
        }
    }
}
