using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSelf : MonoBehaviour
{
    public int delTime;
    // Use this for initialization
    void Start()
    {
        Invoke("destroySelf", delTime);
    }
    void destroySelf()
    {
        Destroy(gameObject);
    }
}