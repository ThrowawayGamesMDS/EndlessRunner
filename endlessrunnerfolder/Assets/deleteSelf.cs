using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSelf : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Invoke("destroySelf", 3);
    }
    void destroySelf()
    {
        Destroy(gameObject);
    }
}