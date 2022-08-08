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
    public AudioClip deathAudio;
    public AudioClip shieldHitAudio;

    //Variables for Shooting
    public static int shotsInReserve{ get; private set; }
    public static int maxShotsInReserve { get; } = 4;
    public static float reload { get; private set; }
    private bool isRecharging = false;

    //Add Game Over event and Loss of Shields event
    public delegate void PlayerDestroyed();
    public static event PlayerDestroyed OnDestruction;

    public delegate void OnShieldValueChange();
    public static event OnShieldValueChange ShieldValueChange;

    public delegate void OnReserveShotChange();
    public static event OnReserveShotChange ShotChange;

    protected override void Awake()
    {
        //Reset Between games
        shotsInReserve = 4;
        reload = 1;
        isRecharging = false;
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        LimitToPlayspace();
        MovePlayer();
        RechargingLaser();
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1")) && shotsInReserve > 0 && LevelManager1.isGameStarted && !LevelManager1.gameOver)
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
        else if (other.gameObject.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
            TakeDamge();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Death();
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
                spacecraftAudioSource.PlayOneShot(shieldHitAudio, 1.0f);
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
        spacecraftAudioSource.PlayOneShot(deathAudio, 0.8f);
        base.Death();
    }
}
