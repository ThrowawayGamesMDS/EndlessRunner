using UnityEngine;

public class follow : MonoBehaviour {
    public Transform playerT;
    public Transform cam;
	void Update ()
    {
        cam.position = new Vector3(0, playerT.position.y + 15f, playerT.position.z - 30);
    }
}
