using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    
    public GameObject player;
    private Transform p_Transform;

	// Use this for initialization
	void Start () {
        p_Transform = player.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        float num = 1;
        Vector3 p1 = new Vector3(p_Transform.position.x, p_Transform.position.y + num, transform.position.z);
        transform.position = Vector3.Lerp(p1, transform.position, 0.1f);
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(p1.x, 0, p1.z);
        }
	}
}
