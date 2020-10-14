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
    public bool storyFlag = false; 
    public static bool displayNotEnoughPoints = false; 
    public Player self; 
    public GameObject mapMenu;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject scrollDisplay1;
    public GameObject scrollDisplay2;
    public GameObject scrollDisplay3;
    public GameObject scrollDisplay4;
    public GameObject scrollDisplay5;
    public GameObject storyDisplay1;
    public GameObject storyDisplay2;
    public GameObject storyDisplay3;
    public GameObject storyDisplay4;
    public GameObject storyDisplay5;
    public GameObject storyDisplay6;
    public GameObject gameOverMenu;
    public GameObject explanationView; 
    public GameObject incorrectCharacter; 
    public GameObject correctExplanationView; 
    public GameObject incorrectExplanationView;
    public GameObject insufficientAbilityPoints; 
    public GameObject replayCharacterView;
    public GameObject advisorsOpinion; 
    public GameObject empireMap; 
    public GameObject peoplesOpinion; 
    public GameObject lieutOpinion; 
    public GameObject councilOpinion; 
    public static int characterCount; 
    public static int storyCount; 
    private string currentScrollDisplay;
    private Text[] mapText;
    public bool dialoguing = false; 

    void Start()
    {
        characterCount = 1;
        storyCount = 1; 
        initialiseMap();
    }

    void Update()
    {
        if (storyCount == 1 && !storyFlag) {
            StartCoroutine(toggleStoryScroll());
        }
        if (Player.CurrentHealth <= 0 && !displayGameOver)
        {
            enterGameOver();
        }

        if (Player.currentAbilityPoints == 0 && displayNotEnoughPoints) {
            enterNotEnoughPoints(); 
        }

        if (Input.GetKeyDown("m") && !gamePaused && !showScroll && !displayGameOver) {
            toggleMap();            
        }
        
        if (Input.GetButtonDown("Cancel") && !mapDisplayed && !showScroll && !displayGameOver) {
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
        if (mapDisplayed || gamePaused || showScroll || displayGameOver || dialoguing)
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
    
    public void toggleScroll()
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
                break; 
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    private IEnumerator toggleStoryScroll() {
        storyFlag = true; 
        yield return new WaitForSeconds(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
        player.GetComponent<FirstPersonController>().enabled = false;
        
        switch (storyCount) {
            case 1:
                storyDisplay1.SetActive(!storyDisplay1.activeInHierarchy);
                yield break;
            case 2:
                storyDisplay2.SetActive(!storyDisplay2.activeInHierarchy);
                yield break;
            case 3:
                storyDisplay3.SetActive(!storyDisplay3.activeInHierarchy);
                yield break;
            case 4:
                storyDisplay4.SetActive(!storyDisplay4.activeInHierarchy);
                yield break;
            case 5:
                storyDisplay5.SetActive(!storyDisplay5.activeInHierarchy);
                yield break;
            case 6:
                storyDisplay6.SetActive(!storyDisplay6.activeInHierarchy);
                yield break;
            default: 
                yield break; 
        }
        StopCoroutine(toggleStoryScroll());
        yield return null;      
    }

    public void enterGameOver()
    {
        displayGameOver = true;
        incorrectExplanationView.SetActive(false); 
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        gameOverMenu.SetActive(!gameOverMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterAdvisorsOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        advisorsOpinion.SetActive(!advisorsOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
        
    }

    public void enterEmpireMap() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        empireMap.SetActive(!empireMap.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterPeoplesOpinion() {
        setScrollDisplaysToFalse();
        //Player.updateAbilityPoints(1); 
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        peoplesOpinion.SetActive(!peoplesOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterLieutOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        lieutOpinion.SetActive(!lieutOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterCouncilOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        councilOpinion.SetActive(!councilOpinion.activeInHierarchy);
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
        setAbilityDisplaysToFalse();
        setStoryDisplaysToFalse(); 

        Cursor.visible = false;
        showScroll = false;
        displayExplanation = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void returnToGameAfterExplain()
    {
        setScrollDisplaysToFalse();       

        Cursor.visible = false;
        showScroll = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;

        storyCount += 1; 

        StartCoroutine(toggleStoryScroll());
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

    public void returnToScroll()
    {
        setAbilityDisplaysToFalse();      

        Cursor.visible = false;
        showScroll = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;

        toggleScroll(); 
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

    public void enterNotEnoughPoints()
    {
        setAbilityDisplaysToFalse(); 
        setScrollDisplaysToFalse();
        displayNotEnoughPoints = false; 

        Time.timeScale = Math.Abs(Time.timeScale - 1);

        insufficientAbilityPoints.SetActive(!insufficientAbilityPoints.activeInHierarchy);
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

    public void setStoryDisplaysToFalse() {
        storyDisplay1.SetActive(false);
        storyDisplay2.SetActive(false);
        storyDisplay3.SetActive(false);
        storyDisplay4.SetActive(false);
        storyDisplay5.SetActive(false);
        storyDisplay6.SetActive(false);
    }

    public void setAbilityDisplaysToFalse() {
        advisorsOpinion.SetActive(false); 
        empireMap.SetActive(false); 
        peoplesOpinion.SetActive(false);
        lieutOpinion.SetActive(false); 
        councilOpinion.SetActive(false);
        insufficientAbilityPoints.SetActive(false);
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


            case "NextLevel":
                requestedSceneIndex = currentBuildIndex + 1;
                changeLevels(currentBuildIndex, requestedSceneIndex);
                break;

            default:
                Debug.Log("something wrong");
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

