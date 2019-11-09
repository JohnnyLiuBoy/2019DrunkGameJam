using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StuffPanel : MonoBehaviour
{
    public GameObject stuffDesc;

    private List<string> stuff;
    private float dt = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.stuff = new List<string>();

        this.stuff.Add("程式\tJohnny");
        this.stuff.Add("程式\tFelix");
        this.stuff.Add("程式、企劃\t不重要");
        this.stuff.Add("美術\t曜憲");
        this.stuff.Add("美術\t栗子");

        // int i = 0;
        // foreach(string item in this.stuff){
        //     GameObject person = Instantiate( this.stuffDesc );

        //     person.transform.SetParent(this.transform);

        //     person.GetComponent<Text>().text = item;

        //     //設置Person位置
        //     person.GetComponent<RectTransform>().transform.localPosition =
        //         new Vector2(0, -i *((RectTransform)person.transform).rect.height
        //         - ((RectTransform)this.transform).rect.height/2); //Vector2(x軸,y軸)
            
        //     i++;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        this.dt += Time.deltaTime;
        
        if(this.dt < 1 || this.stuff.Count <= 0)
            return;
        
        GameObject person = Instantiate( this.stuffDesc );
        person.transform.SetParent(this.transform);
        person.GetComponent<Text>().text = this.stuff[0];
        this.stuff.RemoveAt(0);
        person.GetComponent<RectTransform>().transform.localPosition =
            new Vector2(0, -((RectTransform)this.transform).rect.height/2); //Vector2(x軸,y軸)
        
        if(this.stuff.Count <= 0)
            person.GetComponent<StuffDesc>().SetActionEnd(this.BackToTitle);

        this.dt = 0;
    }

    void BackToTitle(){
        if(this.stuff.Count <= 0)        
            SceneManager.LoadScene("Title");
    }
}
