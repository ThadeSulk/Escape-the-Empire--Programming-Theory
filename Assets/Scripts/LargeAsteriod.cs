using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteriod : Asteriod
{
    private LevelManager1 levelManager1;
    private int partsOnDestruction = 3;

    private void Awake()
    {
        health = 10;
        levelManager1 = GameObject.Find("LevelManager").GetComponent<LevelManager1>();
    }

    public override void Destruction()              //Asteroid brakes into # of smaller asteroids as set above
    {
        for (int i = 0; i < partsOnDestruction; i++)
        {
            levelManager1.SpawnAsteriodOnDestruction(gameObject.transform.position);
        }
    base.Destruction();
        
    }
}
