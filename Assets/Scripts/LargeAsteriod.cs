using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteriod : Asteriod
{
    private SpawnManager_Lvl1 spawnManager_Lvl1;
    private int partsOnDestruction = 3;

    private void Awake()
    {
        spawnManager_Lvl1 = GameObject.Find("SpawnManager").GetComponent<SpawnManager_Lvl1>();
    }

    public override void Destruction()
    {
        Debug.Log("BeforeSpawn");
        for (int i = 0; i < partsOnDestruction; i++)
        {
            Debug.Log("Spawn");
            spawnManager_Lvl1.SpawnAsteriodOnDestruction();
        }
    base.Destruction();
        
    }
}
