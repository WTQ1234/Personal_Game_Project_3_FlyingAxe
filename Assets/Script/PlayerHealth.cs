using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    private Renderer myRender;
    private Animator anim;
    private ScreenFlash sf;
    private Rigidbody2D rb2d;
    private PolygonCollider2D polygonCollider2D;

    void Start()
    {
        BuffManager.Instance.AddBuffByCfgId(gameObject, 0);

        HealthBar.Instance.HealthMax = maxHealth;
        HealthBar.Instance.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void SetMaxHp(int _maxHp)
    {
        maxHealth = _maxHp;
        _OnMaxHpChange();
    }

    public void AddMaxHp(int _addMaxHp)
    {
        maxHealth += _addMaxHp;
        health += _addMaxHp;
        _OnMaxHpChange();
    }

    private void _OnMaxHpChange()
    {
        HealthBar.Instance.HealthCurrent = health;
        HealthBar.Instance.HealthMax = maxHealth;
    }

    public void DamagePlayer(int damage, bool knock_back = false, Transform trans_damage_from = null)
    {
        //sf.FlashScreen();
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        HealthBar.Instance.HealthCurrent = health;
        if (health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            //rb2d.gravityScale = 0.0f;
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(blinks, time);
        //polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    public void HealPlayer(int heal)
    {
        health += heal;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        HealthBar.Instance.HealthCurrent = health;
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        //polygonCollider2D.enabled = true;
    }

    void KillPlayer()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
