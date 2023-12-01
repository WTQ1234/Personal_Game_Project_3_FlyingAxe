using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using HRL;

public class TrapPlatform : MonoBehaviour
{
    public Animator anim;
    private BoxCollider2D bx2D;
    public float animSpeed = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
        bx2D = GetComponent<BoxCollider2D>();
        anim.speed = animSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") 
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            anim.SetBool("Collapse", true);
        }
    }

    void DisableBoxCollider()
    {
        bx2D.enabled = false;
        TimerManager.Instance.AddTimer(ResetPlatform, 2);
    }

    void EnableBoxCollider()
    {
        bx2D.enabled = true;
    }

    void ResetPlatform()
    {
        anim.SetBool("Collapse", false);
    }

    void DestroyTrapPlatform()
    {
        Destroy(gameObject);
    }
}
