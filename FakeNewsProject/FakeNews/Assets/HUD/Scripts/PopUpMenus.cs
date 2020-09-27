using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpMenus : MonoBehaviour
{
    public bool mapDisplayed = false;
    public bool gamePaused = false;
    public bool scrollClicked;
    public bool showScroll = false;
    public bool displayGameOver = false;
    public bool displayExplanation = false; 
    public static bool displayCorrectExplanation = false; 
    public static bool displayIncorrectExplanation = false; 
    public GameObject mapMenu;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject scrollDisplay1;
    public GameObject scrollDisplay2;
    public GameObject scrollDisplay3;
    public GameObject scrollDisplay4;
    public GameObject scrollDisplay5;
    public GameObject gameOverMenu;
    public GameObject explanationView; 
    public GameObject incorrectCharacter; 
    public GameObject correctExplanationView; 
    public GameObject incorrectExplanationView;
    public GameObject replayCharacterView;

    public static int characterCount; 

    private string currentScrollDisplay;

    private Text[] mapText;
    private Button[] mapButtons;

    void Start()
    {
        characterCount = 1;
        initialiseMap();
    }

    void Update()
    {
        if (Player.CurrentHealth <= 0 && !displayGameOver)
        {
            enterGameOver();
        }

        if (Input.GetKeyDown("m") && gamePaused == false && showScroll == false && displayGameOver == false) {
            toggleMap();            
        }
        
        if (Input.GetButtonDown("Cancel") && mapDisplayed == false && showScroll == false && displayGameOver == false) {
            togglePause();            
        }
        
        scrollClicked = SelectObject.scrollClicked;
        if (scrollClicked == true && showScroll == false && mapDisplayed == false && gamePaused == false && displayGameOver == false) {
            if (SelectObject.currentCharacter > characterCount) {
                incorrectCharacterCall(); 
            } else if (SelectObject.currentCharacter < characterCount) {
                replayCharacterCall(); 
            } else {
                toggleScroll();
            }
        } 
        if (mapDisplayed || gamePaused || showScroll || displayGameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.visible = false;
        }
    }

    public void toggleMap()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        mapDisplayed = !mapDisplayed;
        displayMapError(false);
            
        mapMenu.SetActive(!mapMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }


    public void initialiseMap()
    {
        Scene scene = SceneManager.GetActiveScene();

        mapText = GameObject.Find("HUD/MapScreen/MapScreenUI/Text").GetComponentsInChildren<Text>();

        foreach (Text mapText in mapText) {
            if (mapText.name == "CurrentLevelText")
            {
                mapText.text = String.Format("Current Level: {0}", scene.name);
            }
        }
    }


    private void togglePause()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        gamePaused = !gamePaused;
    
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }
    
    private void toggleScroll()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;

        switch (characterCount) {
            case 1: 
                scrollDisplay1.SetActive(!scrollDisplay1.activeInHierarchy);
                break;
            case 2:
                scrollDisplay2.SetActive(!scrollDisplay2.activeInHierarchy);
                break;
            case 3:
                scrollDisplay3.SetActive(!scrollDisplay3.activeInHierarchy);
                break;
            case 4:
                scrollDisplay4.SetActive(!scrollDisplay4.activeInHierarchy);
                break;
            case 5:
                scrollDisplay5.SetActive(!scrollDisplay5.activeInHierarchy);
                break;
            default: 
                Debug.Log("Error in PopUpMenus.toggleScroll()");
                break; 
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterGameOver()
    {
        displayGameOver = true;
        incorrectExplanationView.SetActive(false); 
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        gameOverMenu.SetActive(!gameOverMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }
    
    public void returntoGameFromMap()
    {
        mapMenu.SetActive(false);
        Cursor.visible = false;
        mapDisplayed = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }
    
    public void returntoGameFromPause()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }
    
    public void returnToGameFromScroll()
    {
        setScrollDisplaysToFalse(); 

        Cursor.visible = false;
        showScroll = false;
        displayExplanation = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }
    
    
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        gamePaused = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void incorrectCharacterCall()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
    
        incorrectCharacter.SetActive(!incorrectCharacter.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    private void replayCharacterCall()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
    
        replayCharacterView.SetActive(!replayCharacterView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterCorrectExplanation()
    {
        setScrollDisplaysToFalse(); 

        displayCorrectExplanation = true;
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        correctExplanationView.SetActive(!correctExplanationView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterIncorrectExplanation()
    {
        setScrollDisplaysToFalse(); 

        displayIncorrectExplanation = true;
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        incorrectExplanationView.SetActive(!incorrectExplanationView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void setScrollDisplaysToFalse() {
        scrollDisplay1.SetActive(false);
        scrollDisplay2.SetActive(false);
        scrollDisplay3.SetActive(false);
        scrollDisplay4.SetActive(false);
        scrollDisplay5.SetActive(false);
        correctExplanationView.SetActive(false); 
        incorrectExplanationView.SetActive(false); 
        incorrectCharacter.SetActive(false);
        replayCharacterView.SetActive(false);
    }

    // Returns true if the requested level is not the current, false otherwise. Used for map navigation.
    // todo: Check whether a player has completed all scrolls on a level before allowing access to later ones.
    private void changeLevels(int currentLevel, int nextLevel)
    {
        if (currentLevel != nextLevel)
        {
            SceneManager.LoadScene(nextLevel);
            toggleMap();
        }
        else
        {
            displayMapError(true);
        }
    }

    public void navigateMapLevels(Button button)
    {
        Scene scene = SceneManager.GetActiveScene();
        int currentBuildIndex = scene.buildIndex;
        int requestedSceneIndex;

        switch (button.name)
        {
            case "Level1":
                requestedSceneIndex = 1;

                changeLevels(currentBuildIndex, requestedSceneIndex);

                break;

            case "Level2":
                requestedSceneIndex = 2;

                changeLevels(currentBuildIndex, requestedSceneIndex);

                break;

            case "Level3":
                requestedSceneIndex = 3;

                changeLevels(currentBuildIndex, requestedSceneIndex);

                break;

            case "Level4":
                requestedSceneIndex = 4;

                changeLevels(currentBuildIndex, requestedSceneIndex);

                break;

            case "Level5":
                requestedSceneIndex = 5;

                changeLevels(currentBuildIndex, requestedSceneIndex);
                break;

            case "ReturnToGame":
                break;

            default:
                Debug.Log("somethig wrong pal");
                break;
        }
    }

    private void displayMapError(bool status)
    {
        foreach (Text mapText in mapText)
        {
            if (mapText.name == "LevelChangeError" && status)
            {
                mapText.text = "Already on this level!";
            }
            else if (mapText.name == "LevelChangeError" && !status)
            {
                mapText.text = "";
            }
        }
    }
}

