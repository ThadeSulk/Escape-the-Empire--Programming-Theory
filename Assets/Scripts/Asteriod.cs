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
    private float maxTorque = 1;
    private float xCoordinateDestroy = 24;
    private float zCoordinateDestroy = 20;

    public int health = 3;

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
    public virtual void Destruction()
    {
        Destroy(gameObject);
        LevelManager1.score++;
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteriod"))
        {
            health--;
        }
        else
        {
            health -= 10;
        }
        if (health <= 0) 
        { 
            Destruction(); 
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerLaser") || other.CompareTag("EnemyLaser"))
        {
            health -= 10;
            Destroy(other.gameObject);

            if (health <= 0)
            {
                Destruction();
            }
        }
    }
}
