using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteriod : Asteriod
{
    private LevelManager1 levelManager1;
    private int partsOnDestruction = 3;

    private void Awake()
    {
        levelManager1 = GameObject.Find("LevelManager").GetComponent<LevelManager1>();
    }

    public override void Destruction()
    {
        Debug.Log("BeforeSpawn");
        for (int i = 0; i < partsOnDestruction; i++)
        {
            Debug.Log("Spawn");
            levelManager1.SpawnAsteriodOnDestruction();
        }
    base.Destruction();
        
    }
}
