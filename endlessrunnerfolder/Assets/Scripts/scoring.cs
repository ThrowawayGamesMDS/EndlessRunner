using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring : MonoBehaviour {
    public GameObject popup;
	public bool lockout;
    private string lastObject;
	void OnCollisionEnter(Collision other)
	{
        if(other.gameObject.GetComponent<PointManager>())
        {
            if(other.gameObject.GetComponent<PointManager>().isWorthPoints == true)
            {
                statistics.g_globalPoints += other.gameObject.GetComponent<PointManager>().worth;
                other.gameObject.GetComponent<PointManager>().isWorthPoints = false;
                popup.SetActive(true);
            }
            
        }
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            popup.SetActive(true);
        }
    }

	IEnumerator lockoutf()
	{
		yield return new WaitForSeconds (0.5f);
		lockout = false;
	}
}