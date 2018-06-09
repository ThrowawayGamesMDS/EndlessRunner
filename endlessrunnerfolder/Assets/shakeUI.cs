using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeUI : MonoBehaviour {
    public Text comboXtxt;
    public Text comboValuetxt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(scoring.comboInt < 3)
        {
            comboXtxt.color = Color.white;
            comboValuetxt.color = Color.white;
            iTween.Stop();
        }
        else if(scoring.comboInt >= 3 && scoring.comboInt < 6)
        {
            iTween.ShakePosition(gameObject, iTween.Hash("x", 5, "y", 5));
            comboXtxt.color = Color.yellow;
            comboValuetxt.color = Color.yellow;
        }
        else
        {
            iTween.ShakePosition(gameObject, iTween.Hash("x", 15, "y", 15));
            comboXtxt.color = Color.red;
            comboValuetxt.color = Color.red;
        }
    }
}
