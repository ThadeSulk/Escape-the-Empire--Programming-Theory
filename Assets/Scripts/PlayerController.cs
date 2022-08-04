using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerController : MonoBehaviour
{
    //Player Shields and game over in the Level Manager script
    
    //Player characteristics and Limits
    private float forwardSpeed = 10;
    private float horizontalSpeed = 10;
    private float zLimit = 10;
    private float xLimit = 13;

    private bool isInvincible = false;
    private float invincibilityDuration = 1.0f; //how long is player invincible after getting hit

    //Add Game Over event and Loss of Shields event
    public delegate void PlayerDestroyed();
    public static event PlayerDestroyed OnDestruction;

    public delegate void OnShieldValueChange();
    public static event OnShieldValueChange shieldValueChange;

    // Update is called once per frame
    void Update()
    {
        LimitToPlayspace();
        MovePlayer();
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
                    shieldValueChange?.Invoke();
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
            shieldValueChange?.Invoke();
            
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        //set animation here or in update
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
