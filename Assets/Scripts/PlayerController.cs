using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerController : MonoBehaviour
{
    //Player Shields and game over in the Level Manager script
    
    //Player characteristics and Limits
    private float forwardSpeed = 12;
    private float horizontalSpeed = 12;
    private float zLimit = 10;
    private float xLimit = 13;

    private bool isInvincible = false;
    private float invincibilityDuration = 1.0f; //how long is player invincible after getting hit

    //Variables for Shooting
    [SerializeField] GameObject lazerShotPF;
    [SerializeField]  int shotsInReserve = 4;
    private int maxShotsInReserve = 4;
    [SerializeField] float reload = 0;

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
        RechargingLazer();
        if (Input.GetKeyDown(KeyCode.Space) && shotsInReserve > 0 && LevelManager1.isGameStarted)
        {
            FireLazer();
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
            //Activate shield hit noise

            //Nothing happens to player if they are invincible and they collide with an asteroid
            if (!isInvincible)
            {
                //test if game is over because the player got hit without having shields
                if (LevelManager1.playerShields <= 0)
                {
                    LevelManager1.gameOver = true;
                    Debug.Log("Game Over!");
                    //Play death/gameover noise
                    OnDestruction?.Invoke();                    
                }
                else
                {
                    LevelManager1.playerShields--;
                    ShieldValueChange?.Invoke();
                    StartCoroutine(InvincibilityFrames());
                }
            }
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
        if (other.gameObject.CompareTag("EnemyLazer"))
        {
            Destroy(other.gameObject);
            LevelManager1.playerShields--;
            ShieldValueChange?.Invoke();
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        //set animation here or in update
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    void FireLazer()
    {
        Instantiate(lazerShotPF, gameObject.transform.position + new Vector3(-0.5f, 0, 0), lazerShotPF.transform.rotation);
        Instantiate(lazerShotPF, gameObject.transform.position + new Vector3(0.5f, 0, 0), lazerShotPF.transform.rotation);
        shotsInReserve--;
    }

    void RechargingLazer()
    {
        if (reload < 1)
        {
            reload += Time.deltaTime;
        }

        if (reload >= 1 && shotsInReserve < maxShotsInReserve)
        {
            shotsInReserve++;
            reload = 0;
        }
    }
}
