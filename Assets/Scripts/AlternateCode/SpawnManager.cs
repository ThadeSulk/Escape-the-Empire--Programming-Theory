using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteriodPFs;
    public GameObject[] enemyPFs;
    //private PlayerController playerScript;

    private float xLimitAst = 13;
    private float zPosAst = 15;
    private float xLimitEnemy = 10;
    private float zPosEnemy = -10;

    private float spawnDelayAst = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnAsteriod", spawnDelayAst, spawnDelayAst);
        InvokeRepeating("SpawnAsteriodLarge", spawnDelayAst, spawnDelayAst * 4);
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount < 2)
        {
            SpawnEnemy();
        }
    }

    void SpawnAsteriod()
    {
        if (!LevelManager1.gameOver)
        {
            int setAstSize = Random.Range(0, 2);
            Instantiate(asteriodPFs[setAstSize], RandomAstSpawnPosition(), asteriodPFs[setAstSize].transform.rotation);
        }
    }
    
    void SpawnAsteriodLarge()
    {
        if (!LevelManager1.gameOver)
        {
            Instantiate(asteriodPFs[2], RandomAstSpawnPosition(), asteriodPFs[2].transform.rotation);
        }
    }
    void SpawnEnemy()
    {
        if (!LevelManager1.gameOver)
        {
            int randomEnemyType = Random.Range(0, enemyPFs.Length);
            Instantiate(enemyPFs[randomEnemyType], RandomEnemySpawnPosition(), enemyPFs[randomEnemyType].transform.rotation);
        }
    }

        Vector3 RandomAstSpawnPosition()
    {
        float xPos = Random.Range(-xLimitAst, xLimitAst);
        Vector3 vectorPos = new Vector3(xPos, 0, zPosAst);
        return vectorPos;
    }

    Vector3 RandomEnemySpawnPosition()
    {
        float xPos = Random.Range(-xLimitEnemy, xLimitEnemy);
        Vector3 vectorPos = new Vector3(xPos, 0, zPosEnemy);
        return vectorPos;
    }
}
