using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class animatePopup : MonoBehaviour {
    public float movespeed;
    public CanvasGroup uiElement;
	public bool isTimer;
    
	// Use this for initialization
    void Start()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
        Invoke("destroyself", 2.5f);
    }
	
    void Update()
    {
		if (!isTimer) {
			transform.Translate(0,movespeed,0);
		}
        
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerptime = 2f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerptime;

        while(true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerptime;

            float currentvalue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentvalue;
            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }




	void destroyself()
    {  
        Destroy(gameObject);
    }
}
