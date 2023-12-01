using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float flashTime;

    private Rigidbody2D self_rigidbody;

    public GameObject bloodEffect;
    public GameObject dropCoin;
    public GameObject floatPoint;

    private SpriteRenderer sr; 
    private Color originalColor;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        self_rigidbody = GetComponent<Rigidbody2D>();
        originalColor = sr.color;
    }

    protected virtual void OnEnable()
    {
        health = maxHealth + LevelManager.Instance.level;
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void FixedUpdate()
    {
        _Flip();
    }

    public void TakeDamage(int damage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //GameController.camShake.Shake();

        if (health <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        bool res_health = LevelManager.Instance.OnDropHealth(transform.position);
        if (!res_health)
        {
            LevelManager.Instance.OnDropExp(transform.position);
        }
        bool res = ObjectPoolManager.Instance.ReturnToPool(transform.name, gameObject);
        if (!res)
        {
            Destroy(gameObject);
        }
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    private void _Flip()
    {
        if (self_rigidbody != null)
        {
            bool plyerHasXAxisSpeed = Mathf.Abs(self_rigidbody.velocity.x) > Mathf.Epsilon;
            if (plyerHasXAxisSpeed)
            {
                if (self_rigidbody.velocity.x > 0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

                if (self_rigidbody.velocity.x < -0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
}
