using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            SoundManager.PlayPickCoinClip();
            LevelManager.Instance.AddExp(1);
            CoinUI.CurrentCoinQuantity += 1;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Health"))
        {
            SoundManager.PlayPickCoinClip();
            Player.Instance.playerHealth.HealPlayer(1);
            Destroy(other.gameObject);
        }
    }
}
