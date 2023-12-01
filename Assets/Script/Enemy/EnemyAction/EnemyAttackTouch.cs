using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTouch : MonoBehaviour
{
    public bool destroy_self;
    public int damage;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            print(other.gameObject.name);
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
                if (destroy_self)
                {
                    Destroy(gameObject, 0.1f);
                }
            }
        }
    }
}
