using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EscapeFromTarget : Action
{
    public float speed;
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
            Vector2 dis = target.Value.transform.position - transform.position;
            Vector2 otherTarget = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            otherTarget.Normalize();
            otherTarget += dis.normalized * -0.1f;
            otherTarget.Normalize();
            rb2d.velocity = otherTarget.normalized * speed;
        }
        return TaskStatus.Success;
    }
}
