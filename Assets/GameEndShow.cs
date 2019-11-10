using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndShow : MonoBehaviour
{
    private string description;
    private int curCharacter;
    private float dt;

    // Start is called before the first frame update
    void Start()
    {
        this.curCharacter = 0;
        this.dt = 10;
        this.description = "";

        char heart = '\u2661';
        this.description = "恭喜你 ! 不用擔心房 ‧ 貸 · 囉 " + heart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.curCharacter >= this.description.Length)
            return;
        
        // this.dt += Time.deltaTime;
        // if(this.dt <= 0.05){
        //     return;
        // }
        // this.dt = 0;

        Text t = this.GetComponent<Text>();
        
        if(t != null){
            t.text = this.description.Substring(0, ++curCharacter);
        }
    }
}
