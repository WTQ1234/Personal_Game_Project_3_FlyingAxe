using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBat : Enemy
{
    

    public float speed;
    public Rigidbody2D rb2d;

    private Transform playerTransform;

    private void Update()
    {
        if (Player.Instance != null)
        {
            Vector2 target = Player.Instance.transform.position - transform.position;
            target.Normalize();
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, target * speed, 0.05f);
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
}

