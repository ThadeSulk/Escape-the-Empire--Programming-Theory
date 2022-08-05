using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed = 5;
    private float zCoordinateDestroy = 20;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(gameObject.transform.position.z < -zCoordinateDestroy)
        {
            Destroy(gameObject);
        }
    }
}
