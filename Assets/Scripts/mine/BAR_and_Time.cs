using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BAR_and_Time : MonoBehaviour
{
    public static float Tim;
    void Start()
    {
        Tim = 180;
        En = GameObject.Find("END");
    }
    void Update()
    {
        if (name == "UI_time")
        {
            GetComponent<Text>().text = player_control.Drunk.ToString("#0");
        }
        if (name == "TIME_ARROW")
        {
            if(player_control.start == false)
                Tim -= 1 * Time.deltaTime;
            Vector3 time_arrow = new Vector3(0,0,(Tim / 2f)-90);
            transform.localEulerAngles = time_arrow;
            if(Tim < 0)
            {
                BadEnd();
            }
        }
        if(name == "bar")
        {
            Vector2 bar = transform.localScale;
            bar.y = player_control.Drunk / 1000;
            transform.localScale = bar; 
        } 
    }
    //超時了
    public GameObject En;
    Color Imagecolor = new Color(255, 255, 255, 0);
    public Text EndText;
    void BadEnd()
    {
        player_control.end = 1;
        Imagecolor.r = 255;
        Imagecolor.g = 255;
        Imagecolor.b = 255;
        Imagecolor.a = 255;
        En.GetComponent<Image>().color = Imagecolor;
        if (player_control.Drunk < 200)
            EndText.text = "BAD END4\n\n既不喝酒也不回家，房子被銀行抵押了你也紗世理了\n\n\n\n按下空白鍵重新開始";
        else if (player_control.Drunk < 400)
            EndText.text = "BAD END5\n\n微醺地回家發現房子被抵押後，你跟銀行打了架，結果巴麻美\n\n\n\n按下空白鍵重新開始";
        else if (player_control.Drunk < 600)
            EndText.text = "BAD END6\n\n房子已經被抵押了，而且你也還不夠醉\n\n\n\n按下空白鍵重新開始";
        else if (player_control.Drunk < 800)
            EndText.text = "BAD END7\n\n房子被抵押了，但你也已經脫離現實了\n\n\n\n按下空白鍵重新開始";
        else if (player_control.Drunk < 1000)
            EndText.text = "BAD END8\n\n房子被抵押了，你也已經瘋了\n\n\n\n按下空白鍵重新開始";
        else if (player_control.Drunk > 990)
            EndText.text = "BAD END9\n\n恭喜你，酒精中毒地死在路邊\n\n\n\n按下空白鍵重新開始";
    }
}
