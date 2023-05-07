using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    public SpriteRenderer render = null;

    public float src = 0.0F;
    public float dest =  1.0F;
    public bool reverse = false;
    static float perc = 0.0f;

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(src, dest, perc));
        render.color = color;

        if(reverse)
        {
            perc -= 0.5f * Time.deltaTime;
        }
        else
        {
            perc += 0.5f * Time.deltaTime;
        }

        if(perc >= 1.0f)
        {
            reverse = true;
        }
        else if(perc <= 0)
        {
            reverse = false;
        }
    }
}
