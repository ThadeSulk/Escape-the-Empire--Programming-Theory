using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerController : Spacecraft
{
    //Player Shields and game over in the Level Manager script

    //Player characteristics and Limits
    private float forwardSpeed = 12;
    private float horizontalSpeed = 12;
    private float zLimit = 10;
    private float xLimit = 18;

    //Variables for Shooting
    public static int shotsInReserve{ get; private set; } = 4;
    public static int maxShotsInReserve { get; } = 4;
    public static float reload { get; private set; } = 1;
    private bool isRecharging = false;

    //Add Game Over event and Loss of Shields event
    public delegate void PlayerDestroyed();
    public static event PlayerDestroyed OnDestruction;

    public delegate void OnShieldValueChange();
    public static event OnShieldValueChange ShieldValueChange;

    public delegate void OnReserveShotChange();
    public static event OnReserveShotChange ShotChange;

    // Update is called once per frame
    void Update()
    {
        LimitToPlayspace();
        MovePlayer();
        RechargingLaser();
        if (Input.GetKeyDown(KeyCode.Space) && shotsInReserve > 0 && LevelManager1.isGameStarted)
        {
            FireLaser();
            shotsInReserve--;
            ShotChange?.Invoke();
        }
    }

    //Get directional input and move player
    void MovePlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontalInput * horizontalSpeed, 0, forwardInput * forwardSpeed) * Time.deltaTime);
    }

    // limit player to play area with an if statement for each side
    void LimitToPlayspace()
    {

        if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
        }
        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteriod"))
        {
            TakeDamge();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShieldBoost") && LevelManager1.playerShields < LevelManager1.maxPlayerShields)
        {
            Destroy(other.gameObject);
            LevelManager1.playerShields++;
            ShieldValueChange?.Invoke();
        }
        if (other.gameObject.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
            TakeDamge();
        }
    }

    void RechargingLaser()
    {
        if (reload < 1)
        {
            reload += Time.deltaTime;
            return;
        }

        if (reload >= 1 && shotsInReserve < maxShotsInReserve)
        {
            shotsInReserve++;
            reload = 0;
            isRecharging = true;
            ShotChange?.Invoke();
            return;
        }
        if (reload >=1 && isRecharging)
        {
            ShotChange?.Invoke();
            isRecharging = false;
        }
    }

    void TakeDamge()
    {
        //Nothing happens to player if they are invincible when they collide with an asteroid or get hit with an enemy laser
        if (!isInvincible)
        {
            //test if game is over because the player got hit without having shields
            if (LevelManager1.playerShields <= 0)
            {
                Death();
            }
            else
            {
                //Activate shield hit noise
                LevelManager1.playerShields--;
                ShieldValueChange?.Invoke();
                StartCoroutine(InvincibilityFrames());
            }
        }
    }

    protected override void Death()
    {
        LevelManager1.gameOver = true;
        Debug.Log("Game Over!");
        OnDestruction?.Invoke();
        base.Death();
    }
}
