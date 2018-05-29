using UnityEngine;

public class movement : MonoBehaviour 
{
    public Transform playerT;
    public Vector3 leftV = new Vector3(-0.2f, 0, 0);
    public Vector3 rightV = new Vector3(0.2f, 0, 0);
	void Start()
	{
		Physics.gravity = new Vector3 (0, -150, 0);
	}

    void FixedUpdate()
	{
        if (Input.GetKey("d"))
        {
            playerT.position += rightV;
        }
        if (Input.GetKey("a"))
        {
            playerT.position += leftV;
        }
    }
}
