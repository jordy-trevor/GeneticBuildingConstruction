using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour {

    [Range(0.0f, 10.0f)][SerializeField] private float shakeDistance = 1.0f;
    [Range(0.0f, 10.0f)] [SerializeField] private float speed = 3.0f;
    private Rigidbody rb;
    string moveDirection = "right";

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (this.transform.position.z > shakeDistance)
        {
            moveDirection = "left";
        }
        else if (this.transform.position.z < -1 * shakeDistance)
        {
            moveDirection = "right";
        }

        if (moveDirection == "right")
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        else if (moveDirection == "left")
        {
            rb.MovePosition(transform.position - transform.forward * Time.deltaTime * speed);
        }
       
    }
}
