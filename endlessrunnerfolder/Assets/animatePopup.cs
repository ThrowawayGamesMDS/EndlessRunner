using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class animatePopup : MonoBehaviour {
    public float movespeed;
    public CanvasGroup uiElement;
    public Vector3 startpos;
    
	// Use this for initialization
    void Start()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, 1, 0));
        Invoke("destroyself", 1f);
    }
	
    void Update()
    {
        transform.Translate(0,movespeed,0);
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerptime = 0.3f)
    {
        cg.alpha = 1;
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
