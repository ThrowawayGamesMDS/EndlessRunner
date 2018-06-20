using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTexture : MonoBehaviour {
    public Texture2D[] tex;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", tex[Random.Range(0, tex.Length)]);
    }
	
}
