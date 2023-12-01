using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Health"))
        {
            CoinItem coinItem = other.GetComponent<CoinItem>();
            if (coinItem != null)
            {
                coinItem.Collect();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float radius = GetComponent<CircleCollider2D>().radius;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
