using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed = 5;
    private float zCoordinateDestroy = 20;
    private float rotationSpeed = 50;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, rotationSpeed, 0), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        //destroys item if it leaves the play area
        if (gameObject.transform.position.z < -zCoordinateDestroy)
        {
            Destroy(gameObject);
        }
    }
}
