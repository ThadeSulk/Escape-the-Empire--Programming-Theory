using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GUILevel1 : MonoBehaviour
{
    //Declares display objects;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject confirmMenu;


    private void Update()
    {
        scoreText.text = GameManager.score.ToString();
        shieldText.text = Mathf.Round(LevelManager1.playerShields / LevelManager1.maxPlayerShields * 100).ToString() + "%";

        if (Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
    public void OpenConfirmMenu()
    {
        confirmMenu.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
        confirmMenu.SetActive(false);
    }

    public void LoadMainMenu()                    //Useful if same GUI Script is used in Game, otherwise delete
    {
        SceneManager.LoadScene(0);
    }
}