using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
    public Transform startPos;

    public GameObject TrapPlatform;
    public Transform TrapPos;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var playerHealth = Player.Instance.GetComponent<PlayerHealth>();
                playerHealth.DamagePlayer(1);
                collision.transform.position = startPos.position;
                var trap = GameObject.Instantiate(TrapPlatform, TrapPos.position, Quaternion.identity);
                trap.transform.localScale = Vector3.one;
                trap.GetComponent<Platform>().down = false;
            }
            else if (collision.gameObject.CompareTag("Coin"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Health"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Sickle"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Platform"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
