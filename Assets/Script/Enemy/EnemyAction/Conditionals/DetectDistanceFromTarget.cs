using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DetectDistanceFromTarget : Conditional
{
    public float distance_target = 10;
    public SharedGameObject target;

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            return TaskStatus.Failure;
        }
        Vector2 vec_dis = target.Value.transform.position - transform.position;
        float distance = vec_dis.magnitude;
        return (distance <= distance_target) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
