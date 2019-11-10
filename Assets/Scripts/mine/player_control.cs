using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_control : MonoBehaviour
{
    //開場
    public static bool start = true;
    public GameObject login;
    void Start()
    {
        bottle = GameObject.Find("bottle");
        En = GameObject.Find("END");
        login = GameObject.Find("start");
        rotate_normal_speed = rotate_normal_speed * rotate_speed;
        EndText.text = "";
        ani.SetInteger("ACT", 0);
        start = true;
        Drunk = 0;
    }
    void Update()
    {
        if(end == 0)
        {
            if (Drunk > 1000)
            {
                Drunk = 1000;
            }
            if (canRun == false && threw_up <= 0)
            {
                rotate();
                move();
            }
            run();
            anglespeed();
            drink();
            have_bottle();
            debuff();
        }
        if (start == false)
        {
            if (end == 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Application.LoadLevel("SampleScene");
                }
            }
        }
        if (start == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                end = 0;
                start = false;
                login.SetActive(false);
            }
        }
        if (!Input.GetKey(KeyCode.H)&& !canRun && !Input.GetMouseButton(0)&& !Input.GetKey(KeyCode.UpArrow))
        {
            if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                if (!canJump)
                {
                    ani.SetInteger("ACT", 3);
                }
                else
                {
                    ani.SetInteger("ACT", 0);
                }
            }
        }
        if(transform.position.y < -40)
        {
            end = 1;
            Imagecolor.r = 255;
            Imagecolor.g = 255;
            Imagecolor.b = 255;
            Imagecolor.a = 255;
            En.GetComponent<Image>().color = Imagecolor;
            EndText.text = "TRUE END\n\n我是誰?我在哪裡?我在幹嘛?\n\n\n\n按下空白鍵重新開始";
        }
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
            ani.SetInteger("ACT", 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, move_normal_speed * move_speed * -1 * Time.deltaTime);
            move_way = 2;
            ani.SetInteger("ACT", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(move_normal_speed * move_speed * -1 * Time.deltaTime, 0, 0);
            move_way = 3;
            ani.SetInteger("ACT", 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(move_normal_speed * move_speed * 1 * Time.deltaTime, 0, 0);
            move_way = 4;
            ani.SetInteger("ACT", 1);
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
        if(Input.GetKeyDown(KeyCode.Space)&& !canRun)
        {
            if (canJump == true)
            {
                aso.PlayOneShot(jum);
                ani.SetInteger("ACT", 3);
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
                ani.SetInteger("ACT", 2);
            }
            else if (canRun == true)
            {
                canRun = false;
                move_normal_speed /= 4f;
                ani.SetInteger("ACT", 0);
            }    
        }
        if(canRun == true)
        {
            if(!aso.isPlaying)
                aso.PlayOneShot(ru);
            if (move_way == 1)
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
    public static float Drunk; //喝醉程度
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
                if (!aso.isPlaying)
                    aso.PlayOneShot(drin);
                if (threw_up < 180)
                {
                    ani.SetInteger("ACT", 4);
                    threw_up += 60 * Time.deltaTime;
                    Drunk += (1f * Time.deltaTime * threw_up);
                    if (Drunk > 1000)
                        Drunk = 1000;
                }
                else if (threw_up >= 180)
                {
                    aso.PlayOneShot(too);
                    ani.SetInteger("ACT", 5);
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
            ani.SetInteger("ACT", 0);
            threw_up = 0;
            liqueur = false;
        }
        if (Drunk > 400)
            Drunk -= 3  * Time.deltaTime;
        if (Drunk > 800)
            Drunk -= 5 * Time.deltaTime;
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
                aso.PlayOneShot(get);
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
                ani.SetInteger("ACT", -1);
                aso.PlayOneShot(threw);
                liqueur_bottle = false;
                Instantiate(flybottle,transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        if (liqueur_bottle == true)
            bottle.SetActive(true);
        else
            bottle.SetActive(false);
    }

    //負面效果
    public GameObject die;
    public GameObject die2;
    void debuff()
    {
        if (Drunk < 500)
            die.SetActive(false);
        if (Drunk < 900)
            die2.SetActive(false);
        if (Drunk > 100)
        {
            rotate_speed = Drunk/70;
        }
        if (Drunk > 300)
        {
            int d = 4;
            float r = Random.Range(1, 6);
            if (r == 1)
                transform.Rotate(rotate_normal_speed/d * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 2)
                transform.Rotate(rotate_normal_speed/d * -1 * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 3)
                transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed/d * Time.deltaTime);
            if (r == 4)
                transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed/d * -1 * Time.deltaTime);
            if (r == 5)
                transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed/d * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 6)
                transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed/d * -1 * Time.deltaTime, 0.1F * Time.deltaTime);
        }
        if (Drunk > 500)
        {
            die.SetActive(true);
        }
        if (Drunk > 700)
        {
            camer.speed = 0.001f * Drunk / 10;
        }
        if (Drunk > 900)
        {
            die2.SetActive(true);
            int d = 2;
            float r = Random.Range(1, 6);
            if (r == 1)
                transform.Rotate(rotate_normal_speed / d * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 2)
                transform.Rotate(rotate_normal_speed / d * -1 * Time.deltaTime, 0.1F * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 3)
                transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed / d * Time.deltaTime);
            if (r == 4)
                transform.Rotate(0.01F * Time.deltaTime, 0.01F * Time.deltaTime, rotate_normal_speed / d * -1 * Time.deltaTime);
            if (r == 5)
                transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed / d * Time.deltaTime, 0.1F * Time.deltaTime);
            if (r == 6)
                transform.Rotate(0.1F * Time.deltaTime, rotate_normal_speed / d * -1 * Time.deltaTime, 0.1F * Time.deltaTime);
        }
    }

    //回家
    public static int end = 1;
    public GameObject En;
    public Text EndText;
    Color Imagecolor = new Color(255,255,255,0);
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "floor")
        {
            home();    
        }
    }
    void home()
    {
        end = 1;
        Imagecolor.r = 255;
        Imagecolor.g = 255;
        Imagecolor.b = 255;
        Imagecolor.a = 255;
        En.GetComponent<Image>().color = Imagecolor;
        if (Drunk < 200)
            EndText.text = "BAD END1\n\n你還不夠醉，無法承受現實的打擊，而世以子了\n\n\n\n按下空白鍵重新開始";
        else if (Drunk < 400)
            EndText.text = "BAD END2\n\n還不夠醉，現實的打擊太大了，你就這麼病了整整半年\n\n\n\n按下空白鍵重新開始";
        else if (Drunk < 600)
            EndText.text = "BAD END3\n\n不夠醉，現實的打擊太大了，你就這麼病了一個月\n\n\n\n按下空白鍵重新開始";
        else if (Drunk < 800)
            EndText.text = "GOOD END1\n\n夠醉，你成功逃避了現實，吐了三天就好了\n\n\n\n按下空白鍵重新開始";
        else if (Drunk < 1000)
            EndText.text = "GOOD END2\n\n有夠醉，你完全逃避了現實，心情非常開朗\n\n\n\n按下空白鍵重新開始";
        else if (Drunk > 990)
            EndText.text = "GOOD END2\n\n神，直接酒精中毒，你已經飛到西方極樂世界了\n\n\n\n按下空白鍵重新開始";
    }

    //動畫
    public Animator ani;

    //音效
    public AudioSource aso;
    public AudioClip drin;
    public AudioClip ru;
    public AudioClip jum;
    public AudioClip get;
    public AudioClip too;
    public AudioClip threw;
}
