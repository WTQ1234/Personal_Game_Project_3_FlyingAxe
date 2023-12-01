using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DetectTarget : Conditional
{
    public SharedGameObject target;

    public override TaskStatus OnUpdate()
    {
        return (target.Value != null) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
