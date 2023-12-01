using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InitTarget_Player : Action
{
    public SharedGameObject target;

    public override TaskStatus OnUpdate()
    {
        if (Player.Instance == null)
        {
            return TaskStatus.Failure;
        }
        if (Player.Instance.gameObject == null)
        {
            return TaskStatus.Failure;
        }
        if (target.Value == null)
        {
            target.Value = Player.Instance.gameObject;
        }
        return (target.Value != null) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
