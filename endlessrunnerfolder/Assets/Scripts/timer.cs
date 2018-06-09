using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public Text timerTxt;
    public static float gameTimer;
    public static bool isGameOver;
    public GameObject gameoverobj;
    private void Start()
    {
        isGameOver = false ;
        gameTimer = 60;
    }
    private void Update()
    {
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            gameTimer -= Time.deltaTime;
            timerTxt.text = Mathf.RoundToInt(gameTimer).ToString();
        }

		if (gameTimer <= 0)
        {
            isGameOver = true;
            gameoverobj.SetActive(true);
            ControllerHandler.m_bPlayerIsAlive = false;
            ControllerHandler.m_bIsPaused = true;
            //SceneManager.LoadScene("enter level name here"); 
        }

    }
    
}



