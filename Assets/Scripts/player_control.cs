﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public float rotate_speed;
    public float rotate_normal_speed;

    public float move_speed;
    public float move_normal_speed;

    public float jump_speed;

    public bool canJump = false;
    void Start()
    {
        rotate_normal_speed = rotate_normal_speed * rotate_speed;
        move_normal_speed = move_normal_speed * move_speed;
    }
    void Update()
    {
        rotate();
        move();
    }
    void rotate()
    {
        //操控
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(rotate_normal_speed * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(rotate_normal_speed * -1 * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed * -1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed * Time.deltaTime, 0.1F * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed * -1 * Time.deltaTime, 0.1F * Time.deltaTime);
        }
    }
    void move()
    {
        //操控
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, move_normal_speed * move_speed * 1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, move_normal_speed * move_speed * -1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(move_normal_speed * move_speed * -1 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(move_normal_speed * move_speed * 1 * Time.deltaTime, 0, 0);
        }
    }
    //跳躍
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "ground")
            canJump = true;
    }
    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "ground")
            canJump = false;
    }
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump == true)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 10f * jump_speed, 0);
                GetComponent<Rigidbody>().AddForce(Vector3.up * 5f * jump_speed);
                canJump = false;
            }
        } 
    }
}
