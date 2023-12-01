using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Text expText;
    public Text levelText;
    public static int ExpCurrent;
    public static int ExpMax;
    public static int curLevel;

    private Image expBar;

    void Start()
    {
        expBar = GetComponent<Image>();
    }

    void Update()
    {
        expBar.fillAmount = (float)ExpCurrent / (float)ExpMax;
        expText.text = ExpCurrent.ToString() + "/" + ExpMax.ToString();
        levelText.text = curLevel.ToString();
    }
}
