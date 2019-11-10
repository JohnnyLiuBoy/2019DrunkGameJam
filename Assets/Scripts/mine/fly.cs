using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fly : MonoBehaviour
{
    void Start()
    {
        if(name != "die" && name != "die2")
             Destroy(gameObject, 4);
    }
    void Update()
    {
        if (name != "die" && name != "die2")
            transform.Translate(0.3f, 0.3f, 0.3f); 
        else
            transform.Rotate(0,0,90 * Time.deltaTime);
    }
}
