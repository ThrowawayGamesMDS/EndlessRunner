using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundyPopup : MonoBehaviour {
    public Vector3 m_vec3OriginalPosition;
    public float movespeed;
    public CanvasGroup uiElement;
    public Text m_tPopupToDisplay;
    public bool m_bInitializeAction;

    void ResetPopup()
    {
        m_bInitializeAction = false;
        m_tPopupToDisplay.text = "";
    }

    public void UpdatePopupText(string _sPopupToDisplay)
    {
        m_tPopupToDisplay.text = _sPopupToDisplay;
    }

    // Use this for initialization
    void Start ()
    {
        m_vec3OriginalPosition = this.transform.position;
        m_bInitializeAction = false;
        m_tPopupToDisplay.text = "";

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_tPopupToDisplay.text != "" && !m_bInitializeAction)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
            Invoke("ResetPopup", 2f);
            m_bInitializeAction = true;
        }


        if (m_bInitializeAction) // whilst active move it down le screen
        {
            transform.Translate(0, movespeed, 0);
        }
        else // unactive, need to reposition it.
        {
            this.transform.position = m_vec3OriginalPosition;
            //cg.alpha = 1.0f; // might bug out if the text doesn't get set to nothing accurately
        }
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerptime = 1.5f)
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
