using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SprintTarget : Action
{
    public SharedGameObject target;
    public float sprint_speed = 5f;

    private Rigidbody2D rb2d;
    public override void OnAwake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
        {
            Vector2 vec = target.Value.transform.position - transform.position;
            vec.Normalize();
            rb2d.velocity = vec * sprint_speed;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
