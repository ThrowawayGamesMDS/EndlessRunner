﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public Text timerTxt;
    public static float gameTimer;
    private void Start()
    {
        gameTimer = 60;
    }
    private void Update()
    {
        gameTimer -= Time.deltaTime;
        timerTxt.text = Mathf.RoundToInt(gameTimer).ToString();

		if (gameTimer == 0) {
			//SceneManager.LoadScene("enter level name here"); 
		}

    }

	public void timeIncrease(){
		gameTimer = gameTimer + 5;
	}
}



