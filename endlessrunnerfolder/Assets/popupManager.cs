using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupManager : MonoBehaviour {
	public static List<GameObject> popupQueue;
	[SerializeField] private float timerVal;
	private float timer;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		popupQueue = new List<GameObject> ();
		timer = timerVal;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
		{
			timer -= Time.deltaTime;
		}
		if(timer <= 0 || popupQueue.Count <= 0)
		{
			timer = timerVal;
			if (popupQueue.Count > 0) {
				MakePopup ();
			}
		}
	}
	void MakePopup()
	{
		Instantiate (popupQueue[0], canvas.transform);
		popupQueue.Remove(popupQueue[0]);
	}



}
