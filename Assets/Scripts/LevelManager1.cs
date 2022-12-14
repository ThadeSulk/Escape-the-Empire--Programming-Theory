using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager1 : MonoBehaviour
{
    //public static LevelManager1 Instance;

    [SerializeField] GameObject[] asteriodPFs;
    [SerializeField] GameObject[] largeAsteriodPFs;
    [SerializeField] GameObject[] enemyPFs;
    [SerializeField] GameObject shieldBoost;
    [SerializeField] GameObject inputPanel;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject restartText;

    //Game Variables
    public static int score;
    public static float playerShields;
    public static float maxPlayerShields = 4;
    public static bool isGameStarted;
    public static bool gameOver;
    private bool delay = false;

    //Spawning variables
    private float xLimitAst = 13;
    private float zPosAst = 15;
    private float xLimitEnemy = 10;
    private float zPosEnemy = -14;

    private float baseSpawnDelayAst = 0.5f;
    private float spawnDelayAstLarge = 1f;
    private float spawnDelayShieldBoost = 5f;
    private float spawnDelayAst;
    //private float spawnDelayAstLarge;
    //private float spawnDelayShieldBoost;

    public static float enemyKilledCounter;
    private int maxEnemiesOnScreen;

    void Awake()
    {
        //Resets all static values at begginning of scene
        GameManager.totalScore = 0;                      
        score = 0;
        playerShields = 2;
        isGameStarted = false;
        gameOver = false;
        enemyKilledCounter = 0;
        maxEnemiesOnScreen = 2;
        Time.timeScale = 1; //stops error when reloading scene
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1"))
            {
                isGameStarted = true;
                StartGame();
            }
        }
        else if (gameOver)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1")) && !inputPanel.activeSelf && !delay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Return) && inputPanel.activeSelf)
            {
                string name = inputField.text;
                inputPanel.SetActive(false);
                SaveManager.AddToLeaderboard(new SaveManager.LeaderboardEntry { userName = name, score = score });
                SaveManager.SaveLeaderboard();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount < maxEnemiesOnScreen && isGameStarted)
        {
            SpawnEnemy();
        }
        //This counter system progressively increases the rate of asteroid spawning and total number of enemies on screen
        spawnDelayAst = (float)(baseSpawnDelayAst - .05 * Mathf.Round((enemyKilledCounter - 4) / 10));
        maxEnemiesOnScreen = 2 + Mathf.RoundToInt((enemyKilledCounter - 4) / 10);

    }
    void StartGame()
    {
        startText.SetActive(false);
        Invoke("SpawnAsteriod", 0);
        Invoke("SpawnAsteriodLarge", spawnDelayAstLarge * 5);
        Invoke("SpawnShieldBoost", spawnDelayShieldBoost / 2);
    }

    void OnEnable()                                 //Activates event triggers from playercontroller when LevelManager created
    {
        PlayerController.OnDestruction += GameOver;
    }

    void OnDisable()                                 //Deactivates event triggers from playercontroller when LevelManager disabled
    {
        PlayerController.OnDestruction -= GameOver;
    }

        void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
        Time.timeScale = 0;
        delay = true;
        StartCoroutine(EndDelay());
        if (SaveManager.leaderboardEntries.Count > 0)
        {
            if (score > SaveManager.leaderboardEntries[SaveManager.leaderboardEntries.Count - 1].score || SaveManager.leaderboardEntries.Count != 5)
            {
                inputPanel.SetActive(true);
            }
            else { restartText.SetActive(true);  }
        }
        else            //Turns on inputPanel if there are no saved scores on the leaderboard
        {
            inputPanel.SetActive(true);
        }
    }

    IEnumerator EndDelay() 
    {
        yield return new  WaitForSecondsRealtime(2);
        delay = false;        
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

    public void SpawnAsteriodOnDestruction(Vector3 position)
    {
        //Vector3 position = gameObject.transform.position;
        int setAstSize = Random.Range(0, asteriodPFs.Length);
        int spawnOffsetX = 3;
        int spawnOffsetZ = 2;
        Instantiate(asteriodPFs[setAstSize], position + new Vector3(Random.Range(-spawnOffsetX,spawnOffsetX), 0, Random.Range(-spawnOffsetZ,spawnOffsetZ)),
            asteriodPFs[setAstSize].transform.rotation);

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
