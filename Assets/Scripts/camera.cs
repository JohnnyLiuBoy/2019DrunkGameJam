﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float x;
    public float y;
    public float speed;
    public GameObject player;
    void Update()
    {
        Vector2 a =  Input.mousePosition;
        x = a.x - 1080;
        y = a.y - 540;
        if (x < 0)
            x = x * x * -1;
        else
            x =  x * x;
        if (y < 0)
            y = y * y * -1;
        else
            y = y * y;
        if (a.x < 1080)
        {
            transform.Rotate(0, speed * x *Time.deltaTime,0);
        }
        if (a.x > 1080)
        {
            transform.Rotate(0, speed * x *  Time.deltaTime,0);
        }
        if (a.y < 540)
        {
            transform.Rotate(speed *-1 *y * Time.deltaTime, 0, 0);
        }
        if (a.y > 540)
        {
            transform.Rotate(speed *-1* y * Time.deltaTime, 0, 0);
        }
        if(Input.GetMouseButton(1))
        {
            transform.Rotate(0, 0, 90 * Time.deltaTime);
        }
        Vector3 c = new Vector3(0, 6, -6);
        transform.position = player.transform.position + c;
    }
}
