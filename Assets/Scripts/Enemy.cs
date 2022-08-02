using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    private float speed;
    public string enemyType;    //Name of Enemy Type
    public int enemyHealth = 3;     //Health for Enemy Type
    private float fireRate = .5f;        //bullets per second

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        speed = Random.Range(5, 9);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (player.transform.position.x < transform.position.x)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteriod"))
        {
            enemyHealth--;
            if(enemyHealth < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
