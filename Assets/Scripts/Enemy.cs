using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spacecraft
{
    private GameObject player;

    private float forwardSpeed = 2;
    private float horizontalSpeed = 5;
    public int enemyHealth = 3;     //Health for Enemy Type
    private float delayBeforeFire = 2;
    private float fireRate = .5f;        //bullets per second

    // Start is called before the first frame update
    void Start()
    {
        laserOffset = new Vector3(0.3f, 0, 0);
        invincibilityShield = transform.GetChild(0);
        invincibilityDuration = 1;
        StartCoroutine(InvincibilityFrames());

        player = GameObject.Find("Player");
        horizontalSpeed = Random.Range(5, 9);
        FireSequence();
    }

    // Update is called once per frame
    void Update()
    {
        MovementTypeFollowPlayer();
    }

    void MovementTypeFollowPlayer()
    {
        float xtranslate = 0;
        float ztranslate = forwardSpeed;
        if (player.transform.position.x > transform.position.x)
        {
            xtranslate = horizontalSpeed;
        }
        if (player.transform.position.x < transform.position.x)
        {
            xtranslate = -horizontalSpeed;
        }
        if (transform.position.z < -10)
        {
            ztranslate *= 2;
        }
        if(transform.position.z > player.transform.position.z)
        {
            ztranslate *= -0.5f;
        }

        transform.Translate(new Vector3(xtranslate, 0, ztranslate) * Time.deltaTime);
    }

    protected virtual void FireSequence()
    {
        InvokeRepeating("FireLaser", delayBeforeFire, 1 / fireRate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteriod"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    protected void TakeDamage()
    {
        enemyHealth--;
        if (enemyHealth < 1)
        {
            Death();
        }
    }

    protected override void Death()
    {
        LevelManager1.score += 10;
        Destroy(gameObject);
        base.Death();
    }
}
