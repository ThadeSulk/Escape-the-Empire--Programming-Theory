using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int totalScore;
    public static int playerShields;

    //Makes a singleton of GameManager to exist until the game is exitted
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Leaderboard is automatically saved so it needs to automatically be loaded
        SaveManager.LoadLeaderboard();
    }
}
