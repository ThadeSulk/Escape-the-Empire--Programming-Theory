using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spacecraft
{
    private GameObject player;

    private float speed;
    public int enemyHealth = 3;                     //Health for Enemy Type
    private float delayBeforeFire = 2;
    private float fireRate = .5f;                   //bullets per second

    private float xLimitEnemy = 10;                 //positive/negative limit on random spawn
    private float zPosEnemy = -15;

    private float invincibilityLineZ = -10;         //when the enemy stops being invincible

    // Start is called before the first frame update
    void Start()
    {
        //Alter variables from parent 
        harmfulLazerTag = "PlayerLazer";
        lazerOffset = new Vector3(0.2f, 0, 0);

        //Set new variables
        player = GameObject.Find("Player");
        speed = Random.Range(5, 9);
        InvokeRepeating("FireLazer", delayBeforeFire, 1 / fireRate);

        //Reset Position
        gameObject.transform.position = RandomEnemySpawnPos();
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
        while (gameObject.transform.position.z < invincibilityLineZ) ;
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
    protected override void Death()
    {
        LevelManager1.score += 10;
        base.Death();
    }

    Vector3 RandomEnemySpawnPos()
    {
        float xPos = Random.Range(-xLimitEnemy, xLimitEnemy);
        return new Vector3(xPos, 0, zPosEnemy);
    }
}
