using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffDesc : MonoBehaviour
{
    public int MidFontSize;

    private int startFontSize; 
    private float dt;
    public delegate void OnDestroyCall();
    private OnDestroyCall ff;

    // Start is called before the first frame update
    void Start()
    {
        this.startFontSize = this.GetComponent<Text>().fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        this.dt += Time.deltaTime;

        Transform tt = this.transform;
        RectTransform prt = (RectTransform)tt.parent.transform;
        
        tt.position += (Vector3.up);
        
        if(tt.position.y > prt.rect.height){
            Destroy(this.gameObject);
        }
        // else if(tt.position.y > 100){
        //     tt.localScale -= (Vector3.one * Time.deltaTime);
        // }else if(tt.position.y > -100){
        //     tt.localScale += (Vector3.one * Time.deltaTime);
        // }
    }

    void OnDisable(){
        if(this.ff != null)
            this.ff();
    }

    public void SetActionEnd(OnDestroyCall f){
        this.ff = f;
    }
}
