using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GUIMainMenu : MonoBehaviour
{
    //get leaderboard so it can be turned on and off
    public GameObject leaderboardPanel;
    
    //For creating a leaderboard
    private Transform scoreContainer;
    private Transform scoreTemplate;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LeaderboardToggle()
    {
        if (leaderboardPanel.activeSelf)
        {
            leaderboardPanel.SetActive(false);
        }
        else
        {
            leaderboardPanel.SetActive(true); 
            CreateLeaderboard();            
        }
    }
    public void GUISaveLeaderboard()
    {
        GameManager.SaveLeaderboard();
    }
    public void GUILoadLeaderboard()
    {
        GameManager.LoadLeaderboard();
        CreateLeaderboard();
    }
    //Takes the template in the Score Container, duplicates it and adds take from the LeadershipEntries list in Menu Managers.
    //Does this for each item in the LeadershipEntries list.
    public void CreateLeaderboard()
    {
        List<Transform> leaderboardEntriesTransforms = new List<Transform>();
        scoreContainer = leaderboardPanel.gameObject.transform.Find("ScoreContainer");
        scoreTemplate = scoreContainer.Find("ScoreTemplate");
        scoreTemplate.gameObject.SetActive(false);

        //Deletes old clones of the ScoreTemplate. ScoreTemplate survives because it is deactivated
        foreach (Transform child in scoreContainer.transform)
        {
            if (child.gameObject.activeSelf)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        //Adds a leaderboard onto the GUI for each element in the GameManager's list leaderboardEntires
        foreach (GameManager.LeaderboardEntry leaderboardEntry in GameManager.leaderboardEntries)
        {
            CreateLeaderboardEntryTransform(leaderboardEntry, scoreContainer, leaderboardEntriesTransforms);
        }
    }
    //Adds a leaderboard row onto the GUI for an element in the MenuManager's list leaderboardEntires
    private void CreateLeaderboardEntryTransform(GameManager.LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform scoreTransform = Instantiate(scoreTemplate, container);
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
        scoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count - 50);
        scoreTransform.gameObject.SetActive(true);

        //Sets the position text string based on the entry's position within transformList
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        //sets the text for the entry's three text boxes (Position, Username, Score)
        scoreTransform.Find("PosTemp").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

        string username = leaderboardEntry.userName;
        scoreTransform.Find("UserTemp").GetComponent<TMPro.TextMeshProUGUI>().text = username;

        int score = leaderboardEntry.score;
        scoreTransform.Find("ScoreTemp").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();

        transformList.Add(scoreTransform);
    }
}
