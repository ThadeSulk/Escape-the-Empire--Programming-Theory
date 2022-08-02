using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Lvl1 : MonoBehaviour
{
    public GameObject[] asteriodPFs;
    public GameObject[] enemyPFs;
    public GameObject shieldBoost;
    private PlayerController playerScript;

    private float xLimitAst = 13;
    private float zPosAst = 15;
    private float xLimitEnemy = 10;
    private float zPosEnemy = -10;

    private float spawnDelayAst = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnAsteriod", spawnDelayAst, spawnDelayAst);
        InvokeRepeating("SpawnAsteriodLarge", spawnDelayAst, spawnDelayAst * 4);
        InvokeRepeating("SpawnShieldBoost", spawnDelayAst, spawnDelayAst*10);
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
        if (!playerScript.gameOver)
        {
            int setAstSize = Random.Range(0, 2);
            Instantiate(asteriodPFs[setAstSize], RandomAstSpawnPos(), asteriodPFs[setAstSize].transform.rotation);
        }
    }
    
    void SpawnAsteriodLarge()
    {
        if (!playerScript.gameOver)
        {
            Instantiate(asteriodPFs[2], RandomAstSpawnPos(), asteriodPFs[2].transform.rotation);
        }
    }
    void SpawnEnemy()
    {
        if (!playerScript.gameOver)
        {
            int randomEnemyType = Random.Range(0, enemyPFs.Length);
            Instantiate(enemyPFs[randomEnemyType], RandomEnemySpawnPos(), enemyPFs[randomEnemyType].transform.rotation);
        }
    }
    void SpawnShieldBoost()
    {
        Instantiate(shieldBoost, RandomAstSpawnPos(), shieldBoost.transform.rotation);
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
