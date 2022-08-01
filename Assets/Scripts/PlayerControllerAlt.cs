using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAlt : MonoBehaviour
{
    private Rigidbody playerRB;
    private float forwardInput;
    private float horizontalInput;
    private float forForce = 500;
    private float horForce = 500;

    private float zLimit = 10;
    private float xLimit = 13;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            forwardInput = -0.2f;
        }
        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            forwardInput = 0.2f;
        }
        if (Mathf.Abs(transform.position.x) <= zLimit)
        {
            forwardInput = Input.GetAxis("Vertical");
        }


        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            horizontalInput = 0.2f;
        }
        if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            horizontalInput = -0.2f;
        }
        if (Mathf.Abs(transform.position.x) <= xLimit)
            horizontalInput = Input.GetAxis("Horizontal");

        playerRB.AddForce(new Vector3(horizontalInput * horForce, 0, forwardInput * forForce) * Time.deltaTime);
    }
}
