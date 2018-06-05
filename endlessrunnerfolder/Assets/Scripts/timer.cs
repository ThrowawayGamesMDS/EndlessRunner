using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

	public void timeIncrease(){
		gameTimer = gameTimer + 5;
	}
}



