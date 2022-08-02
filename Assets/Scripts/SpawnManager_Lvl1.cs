using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Lvl1 : MonoBehaviour
{
    public GameObject[] asteriodPFs;
    public GameObject[] largeAsteriodPFs;
    public GameObject[] enemyPFs;
    public GameObject shieldBoost;
    private PlayerController playerScript;

    private float xLimitAst = 13;
    private float zPosAst = 15;
    private float xLimitEnemy = 10;
    private float zPosEnemy = -10;

    private float spawnDelayAst = 0.5f;
    private float spawnDelayAstLarge = 1f;
    private float spawnDelayShieldBoost = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnAsteriod", spawnDelayAst*4);
        Invoke("SpawnAsteriodLarge", spawnDelayAstLarge*3);
        Invoke("SpawnShieldBoost", spawnDelayShieldBoost/2);
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
        if (!playerScript.gameOver)
        {
            int setAstSize = Random.Range(0, largeAsteriodPFs.Length);
            Instantiate(largeAsteriodPFs[setAstSize], RandomAstSpawnPos(), asteriodPFs[setAstSize].transform.rotation);
            Invoke("SpawnAsteriodLarge", spawnDelayAstLarge);
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
