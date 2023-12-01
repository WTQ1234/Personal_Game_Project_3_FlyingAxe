using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShootTarget : Action
{
    public SharedGameObject target;
    public GameObject bullet;
    public float speed = 2f;

    public override TaskStatus OnUpdate()
    {
        GameObject _bullet = GameObject.Instantiate(bullet);
        _bullet.transform.position = transform.position;
        Vector2 dir = target.Value.transform.position - transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //_bullet.transform.LookAt(target.Value.transform);
        Rigidbody2D rb2D = _bullet.GetComponent<Rigidbody2D>();
        rb2D.velocity = (target.Value.transform.position - transform.position).normalized * speed;
        return TaskStatus.Success;
    }
}
