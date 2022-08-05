using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject lazerShotPF;

    private float speed;
    public int enemyHealth = 3;     //Health for Enemy Type
    private float delayBeforeFire = 2;
    private float fireRate = .5f;        //bullets per second

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        speed = Random.Range(5, 9);
        InvokeRepeating("FireLazer", delayBeforeFire, 1 / fireRate);
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
                Death();
            }
        }
    }
    private void Death()
    {
        Destroy(gameObject);
        LevelManager1.score += 10;
    }
    void FireLazer()
    {
        Instantiate(lazerShotPF, gameObject.transform.position + new Vector3(-0.2f, 0, 0), lazerShotPF.transform.rotation);
        Instantiate(lazerShotPF, gameObject.transform.position + new Vector3(0.2f, 0, 0), lazerShotPF.transform.rotation);
    }
}
