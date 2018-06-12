using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameoverscript : MonoBehaviour {
    public CanvasGroup cg;
    public Text score;
    public Text highScore;
    void OnEnable()
    {
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1));
        if(statistics.g_globalPoints > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", statistics.g_globalPoints);
        }
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    void Update()
    {
        score.text = statistics.g_globalPoints.ToString();
        if (timer.isGameOver && Input.GetButtonDown("XBOXStartButton"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerptime = 0.3f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerptime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerptime;

            float currentvalue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentvalue;
            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }


    
}
