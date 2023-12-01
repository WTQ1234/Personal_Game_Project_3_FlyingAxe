using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleMagent : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sickle"))
        {
            Sickle sickle = other.GetComponent<Sickle>();
            if (sickle != null)
            {
                sickle.Collect();
            }
        }
    }
}
