using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float speed = 10;
    private float zCoordinateDestroy = 20;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(gameObject.transform.position.z > zCoordinateDestroy)
        {
            Destroy(gameObject);
        }
    }
}
