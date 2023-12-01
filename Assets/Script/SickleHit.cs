using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HRL;

public class SickleHit : MonoBehaviour
{
    public Sickle sickle;


    public float numProgress = 3;



    public Text text;

    //public List<>

    private void OnEnable()
    {
        InputManager.Instance.RegisterMouseAction(InputOccasion.Update, 0, ButtonType.Up, Shoot);
    }

    private void OnDisable()
    {
        InputManager.Instance.UnRegisterMouseAction(InputOccasion.Update, 0, ButtonType.Up, Shoot);
    }


    void Shoot()
    {
        var sickleInfo = OnGetNextSickleData();
        if (sickleInfo != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            worldPos.z = transform.position.z;
            var sick = Instantiate<Sickle>(sickle, transform.position, transform.rotation);
            sick.Init((worldPos - transform.position).normalized, sickleInfo);
        }
    }

    SickleInfo OnGetNextSickleData()
    {
        if (SickleManager.Instance.OnGetNextSickleData(out SickleInfo sickleData))
        {
            return sickleData;
        }
        return null;
    }
}
