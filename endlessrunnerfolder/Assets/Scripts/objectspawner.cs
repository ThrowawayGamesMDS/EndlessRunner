using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectspawner : MonoBehaviour {
	public List<GameObject> roadObjs;
    public List<GameObject> footpathObjs;
    GameObject rando;
    private Vector3 stopsignPos = new Vector3(15.53f,0.5f,500f);
	void Start()
	{
        InvokeRepeating("SpawnStopSign", 0.1f, 1);


    }
	void Update()
	{
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            spawnRoad();
            spawnPath();
        }
        else
        {
            CancelInvoke();
        }
	}

	void spawnRoad()
	{
		int temp = Random.Range (1, 50);
		if ((temp == 2)) {
			Instantiate (rando = roadObjs [Random.Range (0, roadObjs.Count)], new Vector3 (Random.Range (-11, 11), 1, 500), rando.transform.rotation);	
		}
	}

    void spawnPath()
    {
        int temp = Random.Range(1, 60);
        if ((temp == 2) || (temp == 5))
        {
            Instantiate(rando = footpathObjs[Random.Range(1, footpathObjs.Count)], new Vector3(Random.Range(-17, -22), 1, 500), rando.transform.rotation);
        }
        if ((temp == 24) || (temp == 20))
        {
            Instantiate(rando = footpathObjs[Random.Range(1, footpathObjs.Count)], new Vector3(Random.Range(15, 22), 1, 500), rando.transform.rotation);
        }
    }

    void SpawnStopSign()
    {
        Instantiate(footpathObjs[0], stopsignPos, footpathObjs[0].transform.rotation);
        Instantiate(footpathObjs[0], new Vector3(-17.5f, stopsignPos.y, stopsignPos.z), footpathObjs[0].transform.rotation);

    }
}
