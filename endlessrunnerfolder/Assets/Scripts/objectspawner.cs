using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectspawner : MonoBehaviour {
	public List<GameObject> objs;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	public GameObject obj4;
	public GameObject obj5;
	public GameObject obj6;
	public GameObject obj7;
	public GameObject obj8;
	public GameObject obj9;
    public GameObject obj10;
    GameObject rando;
	void Start()
	{
		objs.Add (obj1);
		objs.Add (obj2);
		objs.Add (obj3);
		objs.Add (obj4);
		objs.Add (obj5);
		objs.Add (obj6);	
		objs.Add (obj7);
		objs.Add (obj8);
		objs.Add (obj9);
        objs.Add (obj10);


    }
	void Update()
	{
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            spawn();
        }
	}

	void spawn()
	{
		int temp = Random.Range (1, 50);
		if ((temp == 2) || (temp == 48) || (temp == 6) || (temp == 25)) {
			Instantiate (rando = objs [Random.Range (0, objs.Count)], new Vector3 (Random.Range (-17, 17), 6, 230), rando.transform.rotation);	
		}
	}


}
