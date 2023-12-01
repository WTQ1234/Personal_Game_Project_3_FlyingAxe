using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_TeleportPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            print(other.gameObject.name);
            Player player = other.gameObject.GetComponent<Player>();
            player.playerHealth.DamagePlayer(1);
            player.ResetPos();
        }
    }
}
