using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public float speed = 3;
    public bool isCollected;

    public void Collect()
    {
        isCollected = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void Update()
    {
        if (isCollected)
        {
            if (Player.Instance != null)
            {
                float distance = (transform.position - Player.Instance.transform.position).sqrMagnitude;

                transform.position = Vector2.MoveTowards(transform.position,
                                                        Player.Instance.transform.position,
                                                        speed * Time.deltaTime);
            }
        }
    }
}
