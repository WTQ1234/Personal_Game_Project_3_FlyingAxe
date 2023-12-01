using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoSingleton<HealthBar>
{
    public Text healthText;
    public int HealthCurrent;
    public int HealthMax;

    public Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
        //HealthCurrent = HealthMax;
    }

    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}
