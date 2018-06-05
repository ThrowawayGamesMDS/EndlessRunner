using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statistics : MonoBehaviour {
    public static int g_globalPoints;
    public Text globalPointsText;
	// Use this for initialization
	void Start () {
        g_globalPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(g_globalPoints < 0)
        {
            g_globalPoints = 0;
        }
        
        globalPointsText.text = g_globalPoints.ToString();
	}
}
