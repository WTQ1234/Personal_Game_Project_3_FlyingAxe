using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;
using UnityEngine.UI;

public class SickleItem : MonoBehaviour
{
    public Image img_bg;
    public Image img_sickle;
    public bool isBack;
    public float rotateSpeed;

    public int sickleId = 0;

    public void InitData(SickleInfo sickleInfo, bool _isBack = false)
    {
        if (sickleId != sickleInfo.Id)
        {
            sickleId = sickleInfo.Id;
            // TODO: 刷新数据显示
        }
        isBack = _isBack;
    }

    public void SetFillPercent(float percent)
    {
        img_sickle.fillAmount = percent;
    }

    void Update()
    {
        RotateSickle();
    }

    void RotateSickle()
    {
        if (isBack)
        {
            img_sickle.transform.Rotate(Vector3.forward * rotateSpeed);
            img_bg.transform.Rotate(Vector3.forward * rotateSpeed);
        }
        else
        {
            img_sickle.transform.rotation = Quaternion.identity;
            img_bg.transform.rotation = Quaternion.identity;
        }
    }
}
