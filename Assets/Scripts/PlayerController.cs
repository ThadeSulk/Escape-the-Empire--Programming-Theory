using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Shields and game over in the Level Manager script
    
    //Player characteristics and Limits
    private float forwardSpeed = 10;
    private float horizontalSpeed = 10;
    private float zLimit = 10;
    private float xLimit = 13;

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

            //test if game is over because the player got hit without having shields
            if (LevelManager1.playerShields <= 0)
            {
                LevelManager1.gameOver = true;
                Debug.Log("Game Over!");
                //Play death/gameover noise
                GameObject.Find("LevelManager").GetComponent<LevelManager1>().GameOver();
            }
            else
            {
                LevelManager1.playerShields--;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShieldBoost") && LevelManager1.playerShields < LevelManager1.maxPlayerShields)
        {
            LevelManager1.playerShields++;
            Destroy(other.gameObject);
        }
    }
}
