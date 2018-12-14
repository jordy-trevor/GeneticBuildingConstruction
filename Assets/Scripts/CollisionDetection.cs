using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

    public bool isColliding = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // report back if touching anything else
    void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    void OnCollisionEnter(Collision other)
    {
        isColliding = true;
    }
}
