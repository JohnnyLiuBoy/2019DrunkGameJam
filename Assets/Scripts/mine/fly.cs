using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fly : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4);
    }
    void Update()
    {
        transform.Translate(0.3f, 0.3f, 0.3f);   
    }
}
