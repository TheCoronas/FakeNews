﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpMenus : MonoBehaviour
{
    public bool mapDisplayed = false;
    public bool inspectDisplayed = false;
    public bool scoreDisplayed = false;
    public bool gamePaused = false;
    public bool showHelp = false;
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
    public GameObject inspectMenu;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject helpDisplay;
    public GameObject scoreDisplay;
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
        // should let you skip logging in whilst debugging
        if (Player.loggedIn == false)
        {
            characterCount = 1;
            Player.userId = 9999999;
            Player.CurrentHealth = Player.maxHealth;
            Player.currentAbilityPoints = Player.maxAbilityPoints;
            Player.currentCoins = Player.maxCoins;
            Player.characterCount = 1;
            Player.activeScene = SceneManager.GetActiveScene().buildIndex;
            
            storyCount = 1;
        }
        else
        {
            storyCount = Player.characterCount;
            characterCount = Player.characterCount;
        }
        initialiseMap();
    }

    void Update()
    {
        // Debug.Log(storyCount);
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

        if (Input.GetKeyDown("m") && !gamePaused && !showScroll && !displayGameOver && !inspectDisplayed && !showHelp && !scoreDisplayed) {
            toggleMap();            
        }

        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown("i") && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed && !showHelp && !scoreDisplayed && scene.buildIndex == 4) {
            toggleInspect();            
        }
        
        if (Input.GetButtonDown("Cancel") && !mapDisplayed && !showScroll && !displayGameOver && !inspectDisplayed && !showHelp && !scoreDisplayed) {
            togglePause();            
        }

        if (Input.GetKeyDown("h") && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed && !inspectDisplayed && !scoreDisplayed)
        {
            toggleHelp();
        }

        // for highscore screen
        if (Input.GetKeyDown("z") && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed && !inspectDisplayed && !showHelp)
        {
            toggleScores();
        }

        adminLevelChange();


        scrollClicked = SelectObject.scrollClicked;
        if (scrollClicked == true && showScroll == false && mapDisplayed == false && gamePaused == false && displayGameOver == false && inspectDisplayed == false) {
            if (SelectObject.currentCharacter > characterCount) {
                incorrectCharacterCall(); 
            } else if (SelectObject.currentCharacter < characterCount) {
                replayCharacterCall(); 
            } else {
                toggleScroll();
            }
        } 
        if (mapDisplayed || gamePaused || showScroll || displayGameOver || inspectDisplayed || dialoguing || scoreDisplayed)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.visible = false;
        }
    }

    public void toggleInspect()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        inspectDisplayed = !inspectDisplayed;
            
        inspectMenu.SetActive(!inspectMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    // Toggle the Map Display on the Screen. Locks the screen and player movement.
    // @author Jaiden Harding
    public void toggleMap()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        mapDisplayed = !mapDisplayed;
        displayMapError(false);
            
        mapMenu.SetActive(!mapMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    public void toggleScores()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        scoreDisplayed = !scoreDisplayed;

        scoreDisplay.SetActive(!scoreDisplay.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    // Informs the player of their current level. Very dependent on the scene heirarchy. Gets the text element and updates with
    // the scene name.
    // @author Jaiden Harding
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

    // Toggle Pause functionality. Locks the screen and player movement. Displays pause menu, capable of resuming or returning to
    // menu.
    // @author Jaiden Harding
    private void togglePause()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        gamePaused = !gamePaused;
    
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    private void toggleHelp()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showHelp = !showHelp;

        helpDisplay.SetActive(!helpDisplay.activeInHierarchy);

        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    public void toggleScroll()
    {
        if (Player.loggedIn)
        {
            saveGame();
        }
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;

        //checks count of character to display different scrolls based on current count
        //@author Madison Beare
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

    //Toggles story scrolls 1 second after an answer has been selected for previous scroll. 
    //This allows for a basic story line to occur automatically without user input
    //@author Madison Beare
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

    //Advisors opinion popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterAdvisorsOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        advisorsOpinion.SetActive(!advisorsOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
        
    }

    //Empire map popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterEmpireMap() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        empireMap.SetActive(!empireMap.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Peoples Opinion popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterPeoplesOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        peoplesOpinion.SetActive(!peoplesOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Lietenant's Opinion popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterLieutOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        lieutOpinion.SetActive(!lieutOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Councils Opinion popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterCouncilOpinion() {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        councilOpinion.SetActive(!councilOpinion.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    // Used for button functionality on the Map Screen. Resumes the game, disables map screen and
    // removes the cursor.
    // @author Jaiden Harding
    public void returntoGameFromMap()
    {
        mapMenu.SetActive(false);
        Cursor.visible = false;
        mapDisplayed = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    //Inspect popup - sets active and removes all other popups
    //@author Madison Beare
    public void returntoGameFromInspect()
    {
        inspectMenu.SetActive(false);
        Cursor.visible = false;
        inspectDisplayed = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    // Used for button functionality on the Pause Screen. Resumes the game, disables pause screen and
    // removes the cursor.
    // @author Jaiden Harding
    public void returntoGameFromPause()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void returntoGameFromHelp()
    {
        helpDisplay.SetActive(false);
        Cursor.visible = false;
        showHelp = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    // Used for button functionality on the Score Screen. Resumes the game, disables score screen and
    // removes the cursor.
    // @author Jaiden Harding
    public void returntoGameFromScore()
    {
        scoreDisplay.SetActive(false);
        Cursor.visible = false;
        scoreDisplayed = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    // Used for button functionality on the Scrolls. Resumes the game, disables map screen and
    // removes the cursor.
    // @author Jaiden Harding, Madison Beare
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

    //Returning to game after an explanation is given - sets active and removes all other popups
    //@author Madison Beare
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

    /** Saves the game state. **/
    public void saveGame()
    {
        if (Player.loggedIn)
        {
            WebClient client = new WebClient();
            var values = new NameValueCollection();
            values["user_id"] = Player.userId.ToString();
            values["currentHealth"] = Player.CurrentHealth.ToString();
            values["abilityPoints"] = Player.currentAbilityPoints.ToString();
            values["activeScene"] = Player.latestScene.ToString();
            values["coins"] = Player.currentCoins.ToString();
            values["characterCount"] = Player.latestCharacterCount.ToString();
            
            byte[] response = client.UploadValues("https://corona.uqcloud.net/test/welcome/save", values);
            var result = Encoding.UTF8.GetString(response);
            Debug.Log(result);
        }
    }
    
    public void returnToMenu()
    {
        saveGame();
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

    //Incorrect Character popup - sets active and removes all other popups
    //@author Madison Beare
    private void incorrectCharacterCall()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
    
        incorrectCharacter.SetActive(!incorrectCharacter.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Replay Character popup - sets active and removes all other popups
    //@author Madison Beare
    private void replayCharacterCall()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
    
        replayCharacterView.SetActive(!replayCharacterView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Enter Correct Explanation popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterCorrectExplanation()
    {
        setScrollDisplaysToFalse(); 

        displayCorrectExplanation = true;
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        correctExplanationView.SetActive(!correctExplanationView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Enter Incorrect Explanation popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterIncorrectExplanation()
    {
        setScrollDisplaysToFalse(); 

        displayIncorrectExplanation = true;
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        incorrectExplanationView.SetActive(!incorrectExplanationView.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    //Not Enough Points popup - sets active and removes all other popups
    //@author Madison Beare
    public void enterNotEnoughPoints()
    {
        setAbilityDisplaysToFalse(); 
        setScrollDisplaysToFalse();
        displayNotEnoughPoints = false; 

        Time.timeScale = Math.Abs(Time.timeScale - 1);

        insufficientAbilityPoints.SetActive(!insufficientAbilityPoints.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }
    
    //Set's all displays to false when function is called. All are set for brevity. 
    //@author Madison Beare
    //@author Jaiden Harding
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
        
        // todo temp dom
        // if (SceneManager.GetActiveScene().buildIndex != 4)
        // {
        //     return;
        // }
        inspectMenu.SetActive(false);
    }

    // Returns true if the requested level is not the current, false otherwise. Used for map navigation on the map screen.
    // Sets the character count depending on whether the level is above or below the current.
    // @author Jaiden Harding, Dom Zhu
    // todo: Check whether a player has completed all scrolls on a level before allowing access to later ones.
    private void changeLevels(int currentLevel, int nextLevel)
    {
        if (currentLevel != nextLevel)
        {
            SceneManager.LoadScene(nextLevel);
            
            // if go to previous level
            if (Player.latestScene > nextLevel)
            {
                // character count determines who talks - if you go to previous level
                // characters should have their scrolls already done.
                Player.characterCount = 999;
            }
            // return to current progression
            else if (nextLevel == Player.latestScene)
            {
                Player.characterCount = Player.latestCharacterCount;
            } else if (nextLevel > Player.latestScene) // go to next level - todo need to check if you've completed previous scrolls 
            // maybe charactercount > that levels max character count
            {
                Player.latestScene = nextLevel;
                Player.characterCount = 1;
                Player.latestCharacterCount = Player.characterCount;
            }
        }
        else
        {
            displayMapError(true);
        }
    }

    // Used for the button functionality on the Map Screen. Depending on the button that is clicked, the relevant scene indexes are
    // generated. This relies on Levels 1 through 5 being their respective numbers within the build settings.
    // @author Jaiden Harding
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

    // If the player selects the level that they are currently on, an error will display on the Map Screen.
    // @author Jaiden Harding
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


    // Used as an admin function to quickly change between levels. [ to go back ] goes forward.
    // @author Jaiden Harding
    private void adminLevelChange()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Goes forward
        if (Input.GetKeyDown("["))
        {
            int currentLevel = scene.buildIndex;
            if (currentLevel != 1)
            {
                changeLevels(currentLevel, currentLevel - 1);
            }
        }

        // Goes back
        if (Input.GetKeyDown("]"))
        {
            int currentLevel = scene.buildIndex;
            if (currentLevel != 5)
            {
                changeLevels(currentLevel, currentLevel + 1);
            }
        }
    }
}

