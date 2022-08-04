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
    [SerializeField] Transform shieldIconParent;
    [SerializeField] Transform shieldIconTransformTemplate;

    //Displays shield icons 
    private List<Transform> shieldIconTransformList = new List<Transform>();

    private void Start()
    {
        //Creates a list of shield icons that can be turned on and off to show shield status
        //Transform shieldIconTransformTemplate = shieldIconParent.GetComponentInChildren<Transform>();
        shieldIconTransformList.Add(shieldIconTransformTemplate);
        for(int i = 1; i < LevelManager1.maxPlayerShields; i++)
        {
            float templateWidth = 25;
            Transform shieldIconTransform = Instantiate(shieldIconTransformTemplate, shieldIconParent);
            shieldIconTransform.GetComponent<RectTransform>().anchoredPosition += new Vector2(templateWidth * shieldIconTransformList.Count, 0);
            shieldIconTransformList.Add(shieldIconTransform);
            if(shieldIconTransformList.Count > LevelManager1.playerShields)
            {
                shieldIconTransform.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        scoreText.text = LevelManager1.score.ToString();
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
    
    void OnEnable()                                 //Activates event triggers from playercontroller when LevelManager created
    {
        PlayerController.shieldValueChange += ChangeShieldGUI;
    }

    void OnDisable()                                 //Deactivates event triggers from playercontroller when LevelManager disabled
    {
        PlayerController.shieldValueChange -= ChangeShieldGUI;
    }

    void ChangeShieldGUI()                          //Turns shield icons on and off when shield event is triggered, confirms shield icon exists first
    {        
        if (Mathf.RoundToInt(LevelManager1.playerShields) >= 1)
        {
            shieldIconTransformList[Mathf.RoundToInt(LevelManager1.playerShields - 1)].gameObject.SetActive(true);
        }
        if (shieldIconTransformList[Mathf.RoundToInt(LevelManager1.playerShields)] != null)
        {
            shieldIconTransformList[Mathf.RoundToInt(LevelManager1.playerShields)].gameObject.SetActive(false);
        }
    }
}