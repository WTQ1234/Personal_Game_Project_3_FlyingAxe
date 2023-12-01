using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Vector3 StartPos;

    public CoinMagnet coinMagnet;
    public PlayerHealth playerHealth;
    public PlayerController controller;
    public ScreenZoomBlur screenZoomBlur;

    public ParticleSystem particleSystem_levelUp;

    public float radious_collectCoin;

    void Awake()
    {
        Instance = this;
        StartPos = transform.position;
    }

    public void OnLevelUp()
    {
        particleSystem_levelUp.Play();
    }

    public void ResetPos()
    {
        transform.position = StartPos;
    }
}
