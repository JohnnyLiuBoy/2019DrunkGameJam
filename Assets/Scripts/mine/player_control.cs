using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    void Start()
    {
        bottle = GameObject.Find("bottle");
        rotate_normal_speed = rotate_normal_speed * rotate_speed;
    }
    void Update()
    {
        if(canRun == false && threw_up <= 0)
        {
            rotate();
            move();
        }
        run();
        anglespeed();
        drink();
        have_bottle();
    }
    //旋轉
    public float rotate_speed = 1;//特殊旋轉速度
    public float rotate_normal_speed = 60;//基本旋轉速度
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
    //移動
    public int move_way = 1;//移動方向
    public float move_speed;//特殊移動速度
    public float move_normal_speed = 30;//基本移動速度
    void move()
    {
        //操控
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, move_normal_speed * move_speed * 1 * Time.deltaTime);
            move_way = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, move_normal_speed * move_speed * -1 * Time.deltaTime);
            move_way = 2;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(move_normal_speed * move_speed * -1 * Time.deltaTime, 0, 0);
            move_way = 3;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(move_normal_speed * move_speed * 1 * Time.deltaTime, 0, 0);
            move_way = 4;
        }
    }
    //跳躍
    public float jump_speed = 1;//跳躍高度
    public bool canJump = false;//可以跳躍
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
    //衝刺
    public bool canRun = false;//跑步中
    void run()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if (canRun == false)
            {
                canRun = true;
                move_normal_speed *= 4;
            }
            else if (canRun == true)
            {
                canRun = false;
                move_normal_speed /= 4f;
            }    
        }
        if(canRun == true)
        {
            if(move_way == 1)
                transform.Translate(0, 0, move_normal_speed * move_speed * 1 * Time.deltaTime);
            if (move_way == 2)
                transform.Translate(0, 0, move_normal_speed * move_speed * -1 * Time.deltaTime);
            if (move_way == 3)
                transform.Translate(move_normal_speed * move_speed * -1 * Time.deltaTime, 0, 0);
            if (move_way == 4)
                transform.Translate(move_normal_speed * move_speed * 1 * Time.deltaTime, 0, 0);
        }
    }
    //角度與速度
    public float x;//滑鼠X軸座標
    public float z;//滑鼠Z軸座標
    void anglespeed()
    {
        x = transform.rotation.x;
        z = transform.rotation.z;
        if (x < 0)
            x *= -1;
        if (z < 0)
            z *= -1;
        x = 2 - x;
        z = 2 - z;
        float cc = x * z * x * z;
        move_speed = cc * cc  /300;
    }
    //喝酒
    public bool liqueur = false; //酒瓶有酒嗎
    public bool liqueur_bottle = false; //有拿酒瓶嗎
    public float threw_up; //連續喝多久
    public float Drunk; //喝醉程度
    void drink()
    {
        if (Input.GetMouseButtonDown(0))
        {
            threw_up = 0;
        }
        if (Input.GetMouseButton(0))
        { 
            if (liqueur == true && liqueur_bottle == true)
            {
                if(threw_up < 180)
                {
                    threw_up += 60 * Time.deltaTime;
                    Drunk += (1f * Time.deltaTime * threw_up);
                }
                else if (threw_up >= 180)
                {
                    Drunk -= 400;
                    threw_up = 0;
                    liqueur = false;
                    if (Drunk < 0)
                        Drunk = 0;
                    if (threw_up > 240)
                    {
                        Drunk = 0;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            threw_up = 0;
            liqueur = false;
        }
    }
    //拿酒丟酒
    public GameObject bottle;
    public GameObject flybottle;
    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "map_bottle")
        {
            if (Input.GetKey(KeyCode.H)&& liqueur_bottle == false)
            {
                Destroy(c.gameObject);
                liqueur_bottle = true;
                liqueur = true;
            }
        }    
    }
    void have_bottle()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (liqueur_bottle == true && liqueur == false)
            {
                liqueur_bottle = false;
                Instantiate(flybottle,transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        if (liqueur_bottle == true)
            bottle.SetActive(true);
        else
            bottle.SetActive(false);
    }
}
