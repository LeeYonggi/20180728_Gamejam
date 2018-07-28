using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private Rigidbody rb;

    public int maxPatternNum;
    public int[] notePattern;

    public float gravityScale = -10.0f;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
	}

    private void Update()
    {
        
    }

    void FixedUpdate ()
    {
        rb.AddForce(new Vector3(0, gravityScale, 0), ForceMode.Acceleration);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
