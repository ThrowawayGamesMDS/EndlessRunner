using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class animatePopup : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        iTween.ShakePosition(gameObject, iTween.Hash("x", 10.3f, "y", 10.3f, "time", 1.5f));
        Invoke("destroyself", 1.5f);
        
    }
	
	void destroyself()
    {
        iTween.Stop();
        gameObject.SetActive(false); 
    }
}
