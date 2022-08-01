using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    private Rigidbody asteriodRB;
    private Vector3 moveDirection;
    private float initialSpeed = 10;
    private float continuousSpeed = 1;
    private float maxXdirection = 0.3f;
    private float maxTorque;
    private float xCoordinateDestroy = 20;
    private float zCoordinateDestroy = 20;

    // Start is called before the first frame update
    void Start()
    {
        asteriodRB = GetComponent<Rigidbody>();
        moveDirection = new Vector3(Random.Range(-maxXdirection, maxXdirection), 0 , -1);

        asteriodRB.AddForce(moveDirection * initialSpeed, ForceMode.Impulse);
        asteriodRB.AddTorque(RandomTorqueVector(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        asteriodRB.AddForce(moveDirection * continuousSpeed);

        if(Mathf.Abs(transform.position.x) > xCoordinateDestroy || Mathf.Abs(transform.position.z) > zCoordinateDestroy)
        {
            Destroy(gameObject);
        }
    }
    Vector3 RandomTorqueVector()
    {
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
    }

}
