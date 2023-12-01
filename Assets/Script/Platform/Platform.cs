using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int width = 3;
    public float downSpeed = 1f;
    public bool down = true;

    void Update()
    {
        if (down)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }
    }
}
