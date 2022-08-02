using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GUILevel1 : MonoBehaviour
{
    private GameObject display;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        display = GameObject.Find("UI Display");
        scoreText = display.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = GameManager.score.ToString();
    }


    public void ReturnMenu()                    //Useful if same GUI Script is used in Game, otherwise delete
    {
        SceneManager.LoadScene(0);
    }


}
