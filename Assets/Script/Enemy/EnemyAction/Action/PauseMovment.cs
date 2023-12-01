using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PauseMovment : Action
{
    public float pause_time = 0.5f;

    public float cur_time = 0;

    private Rigidbody2D rb2d;
    public override void OnAwake()
    {
        base.OnAwake();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public override void OnStart()
    {
        base.OnStart();
        cur_time = 0;
    }

    public override TaskStatus OnUpdate()
    {
        cur_time += Time.deltaTime;
        if (cur_time > pause_time)
        {
            cur_time = 0;
            return TaskStatus.Success;
        }
        rb2d.velocity = Vector2.zero;
        return TaskStatus.Running;
    }
}
