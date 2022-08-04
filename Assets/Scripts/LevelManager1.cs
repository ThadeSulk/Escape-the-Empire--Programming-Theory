using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager1 : MonoBehaviour
{
    public static LevelManager1 Instance;

    public GameObject[] asteriodPFs;
    public GameObject[] largeAsteriodPFs;
    public GameObject[] enemyPFs;
    public GameObject shieldBoost;
    public GameObject inputPanel;
    public TMP_InputField inputField;

    //Game Variables
    public static int score;
    public static float playerShields;
    public static float maxPlayerShields = 4.001f;
    public bool isGameStarted;
    public static bool gameOver;

    private float xLimitAst = 13;
    private float zPosAst = 15;
    private float xLimitEnemy = 10;
    private float zPosEnemy = -10;

    private float spawnDelayAst = 0.5f;
    private float spawnDelayAstLarge = 1f;
    private float spawnDelayShieldBoost = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        //Resets all static values at begginning of scene
        GameManager.score = 0;                      
        score = 0;
        playerShields = 2;
        isGameStarted = false;
        gameOver = false;
        Time.timeScale = 1; //stops error when reloading scene
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGameStarted = true;
                StartGame();
            }
        }
        else if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !inputPanel.activeSelf)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Load Scene, no panel");
            }
            if (Input.GetKeyDown(KeyCode.Return) && inputPanel.activeSelf)
            {
                string name = inputField.text;
                inputPanel.SetActive(false);
                SaveManager.AddToLeaderboard(new SaveManager.LeaderboardEntry { userName = name, score = score });
                SaveManager.SaveLeaderboard();
                Debug.Log("Load Scene, Panel");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount < 2 && isGameStarted)
        {
            SpawnEnemy();
        }
    }
    void StartGame()
    {
        Invoke("SpawnAsteriod", spawnDelayAst * 4);
        Invoke("SpawnAsteriodLarge", spawnDelayAstLarge * 3);
        Invoke("SpawnShieldBoost", spawnDelayShieldBoost / 2);
    }
    public void GameOver()
    {
        gameOver = true;
        //GameOverText.SetActive(true);
        Time.timeScale = 0;
        if (SaveManager.leaderboardEntries.Count > 0)
        {
            if (score > SaveManager.leaderboardEntries[SaveManager.leaderboardEntries.Count - 1].score || SaveManager.leaderboardEntries.Count != 5)
            {
                inputPanel.SetActive(true);
            }

        }
        else            //Turns on inputPanel if there are no saved scores on the leaderboard
        {
            inputPanel.SetActive(true);
        }

    }
    void SpawnAsteriod()
    {
        if (!gameOver)
        {
            int setAstSize = Random.Range(0, asteriodPFs.Length);
            Instantiate(asteriodPFs[setAstSize], RandomAstSpawnPos(), asteriodPFs[setAstSize].transform.rotation);
            Invoke("SpawnAsteriod", spawnDelayAst);
        }
    }

    public void SpawnAsteriodOnDestruction()
    {
        //Vector3 position = gameObject.transform.position;
        int setAstSize = Random.Range(0, asteriodPFs.Length);
        Instantiate(asteriodPFs[setAstSize], gameObject.transform.position, asteriodPFs[setAstSize].transform.rotation);

    }

    void SpawnAsteriodLarge()
    {
        if (!gameOver)
        {
            int setAstSize = Random.Range(0, largeAsteriodPFs.Length);
            Instantiate(largeAsteriodPFs[setAstSize], RandomAstSpawnPos(), asteriodPFs[setAstSize].transform.rotation);
            Invoke("SpawnAsteriodLarge", spawnDelayAstLarge);
        }
    }

    void SpawnEnemy()
    {
        if (!gameOver)
        {
            int randomEnemyType = Random.Range(0, enemyPFs.Length);
            Instantiate(enemyPFs[randomEnemyType], RandomEnemySpawnPos(), enemyPFs[randomEnemyType].transform.rotation);
        }
    }
    void SpawnShieldBoost()
    {
        Instantiate(shieldBoost, RandomAstSpawnPos(), shieldBoost.transform.rotation);
        Invoke("SpawnShieldBoost", spawnDelayShieldBoost);
    }

    Vector3 RandomAstSpawnPos()
    {
        float xPos = Random.Range(-xLimitAst, xLimitAst);
        return new Vector3(xPos, 0, zPosAst);
    }

    Vector3 RandomEnemySpawnPos()
    {
        float xPos = Random.Range(-xLimitEnemy, xLimitEnemy);
        return new Vector3(xPos, 0, zPosEnemy);
    }
}
