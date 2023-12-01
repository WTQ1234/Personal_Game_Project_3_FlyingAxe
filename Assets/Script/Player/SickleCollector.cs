using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleCollector : MonoBehaviour
{
    public SickleHit sickleHit;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sickle"))
        {
            Sickle sickle = other.GetComponent<Sickle>();
            if (sickle != null)
            {
                if (!sickle.back)
                {
                    return;
                }
            }
            SickleManager.Instance.OnRecycleSickle(sickle);
            SoundManager.PlayPickCoinClip();
            Destroy(other.gameObject);
        }
    }
}
