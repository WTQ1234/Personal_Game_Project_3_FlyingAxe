using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FollowTarget : Action
{
    public float speed;
    public float break_distance;
    public SharedGameObject target;

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
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, vec * speed, 0.05f);
        }
        else
        {
            return TaskStatus.Failure;
        }
        Vector2 dis = target.Value.transform.position - transform.position;
        if (dis.magnitude <= break_distance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
