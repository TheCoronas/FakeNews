using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class PopUpMenus : MonoBehaviour
{
    private float timeSinceClicked;
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
    public GameObject scoreScreenUI;
    public GameObject mapMenu;
    public GameObject inspectMenu;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject helpDisplay;
    public GameObject scoreContentParent;
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
    public GameObject advisorsOpinion1;
    public GameObject advisorsOpinion2;
    public GameObject advisorsOpinion3;
    public GameObject advisorsOpinion4;
    public GameObject advisorsOpinion5;
    public GameObject empireMap1;
    public GameObject empireMap2;
    public GameObject empireMap3;
    public GameObject empireMap4;
    public GameObject empireMap5;
    public GameObject peoplesOpinion1;
    public GameObject peoplesOpinion2;
    public GameObject peoplesOpinion3;
    public GameObject peoplesOpinion4;
    public GameObject peoplesOpinion5;
    public GameObject lieutOpinion1;
    public GameObject lieutOpinion2;
    public GameObject lieutOpinion3;
    public GameObject lieutOpinion4;
    public GameObject lieutOpinion5;
    public GameObject councilOpinion1;
    public GameObject councilOpinion2;
    public GameObject councilOpinion3;
    public GameObject councilOpinion4;
    public GameObject councilOpinion5;
    public GameObject ScoreText;
    public static int characterCount;
    public static int storyCount;
    private string currentScrollDisplay;
    private Text[] mapText;
    public bool dialoguing = false;
    private Stopwatch sw;
    [System.Serializable]
    public class UserData
    {
        public string user_id;
        public string username;
        public string currentHealth;
        public string coins;
        public string abilityPoints;
    }

    [System.Serializable]
    public class ScoreJSON
    {
        public UserData[] userInfo;
    }

    /** Populates the scoreboard. **/
    private void renderScores()
    {
        // Retrieve the scores
        WebClient client = new WebClient();
        var values = new NameValueCollection();
        values["user_id"] = Player.userId.ToString();
        byte[] response = client.UploadValues("https://corona.uqcloud.net/test/welcome/getScores", values);
        var result = Encoding.UTF8.GetString(response);

        Debug.Log(result);

        var v = JsonUtility.FromJson<ScoreJSON>("{\"userInfo\":" + result + "}");

        // Populate the scoreboard
        int count = 1;
        foreach (UserData user in v.userInfo)
        {
            // initialise each score
            GameObject o = new GameObject();
            o.transform.SetParent(scoreContentParent.transform);
            o.AddComponent<Text>();
            o.AddComponent<ContentSizeFitter>();
            o.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            o.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            o.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            o.GetComponent<RectTransform>().pivot = new Vector2(0, (float)0.5);
            o.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);

            char[] text = new char[70];

            // For the player position
            String countStr = count.ToString();
            for (int i = 0; i < countStr.Length; ++i)
            {
                text[i] = countStr[i];
            }

            // save player position to inform of change in rank
            if (user.user_id == Player.userId.ToString())
            {
                int rankGain = Player.ranking - count;

                if (Player.ranking != -1)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    if (rankGain > 0)
                    {
                        EditorUtility.DisplayDialog("Class Rank", $"Your rank has increased by {rankGain}. You are now rank {count}", "Ok");
                    }
                    else if (rankGain == 0)
                    {
                        EditorUtility.DisplayDialog("Class Rank", $"Your rank has not changed. You are still rank {Player.ranking}", "Ok");
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Class Rank", $"Your rank has decreased by {rankGain}. You are now rank {count}", "Ok");
                    }
                }
                Player.ranking = count;
            }

            text[countStr.Length] = '.';

            // set username
            for (int i = 0; i < user.username.Length; ++i)
            {
                text[i + 3] = user.username[i];
            }

            // set empire health
            for (int i = 0; i < user.currentHealth.Length; ++i)
            {
                text[i + 16] = user.currentHealth[i];
            }

            // set coins
            for (int i = 0; i < user.coins.Length; ++i)
            {
                text[i + 26] = user.coins[i];
            }


            // set ability points
            for (int i = 0; i < user.abilityPoints.Length; ++i)
            {
                text[i + 34] = user.abilityPoints[i];
            }


            // replace nulls
            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] == '\0')
                {
                    text[i] = ' ';
                }
            }
            o.GetComponent<Text>().text = new string(text);
            o.GetComponent<Text>().font = ScoreText.GetComponent<Text>().font;
            o.GetComponent<Text>().fontSize = 20;
            count++;
        }

        Canvas.ForceUpdateCanvases();
    }

    /** Occurs when script is first run. **/
    void Start()
    {
        // initialise toggle scroll delay
        sw = new Stopwatch();
        sw.Start();

        scoreScreenUI.SetActive(false);
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

            renderScores();
            GUI.FocusWindow(0);
            Cursor.visible = false;
        }
        initialiseMap();

    }

    /** Occurs once for each frame. **/
    void Update()
    {
        // Debug.Log(storyCount);
        if (storyCount == 1 && !storyFlag)
        {
            StartCoroutine(toggleStoryScroll());
        }
        if (Player.CurrentHealth <= 0 && !displayGameOver)
        {
            enterGameOver();
        }

        if (Player.currentAbilityPoints == 0 && displayNotEnoughPoints)
        {
            enterNotEnoughPoints();
        }

        if (Input.GetKeyDown("m") && !gamePaused && !showScroll && !displayGameOver && !inspectDisplayed && !showHelp && !scoreDisplayed)
        {
            toggleMap();
        }

        Scene scene = SceneManager.GetActiveScene();
        int currentBuildIndex = scene.buildIndex;
        if (Input.GetKeyDown("i") && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed && !showHelp && !scoreDisplayed && scene.buildIndex == 4)
        {
            toggleInspect();
        }

        if (Input.GetButtonDown("Cancel") && !mapDisplayed && !showScroll && !displayGameOver && !inspectDisplayed && !showHelp && !scoreDisplayed)
        {
            togglePause();
        }

        if (Input.GetKeyDown("h") && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed && !scoreDisplayed)
        {
            toggleHelp();
        }

        // for highscore screen
        if (Input.GetKeyDown("z") && Player.loggedIn && !gamePaused && !showScroll && !displayGameOver && !mapDisplayed)
        {
            toggleScores();
        }

        scrollClicked = SelectObject.scrollClicked;
        if (scrollClicked == true && showScroll == false && mapDisplayed == false && gamePaused == false && displayGameOver == false && inspectDisplayed == false && !scoreDisplayed)
        {
            if (SelectObject.currentCharacter > characterCount)
            {
                incorrectCharacterCall();
            }
            else if (SelectObject.currentCharacter < characterCount)
            {
                replayCharacterCall();
            }
            else
            {
                toggleScroll();
            }
        }
        if (mapDisplayed || gamePaused || showScroll || displayGameOver || inspectDisplayed || dialoguing || scoreDisplayed || showHelp)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
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

    public void toggleMap()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        mapDisplayed = !mapDisplayed;
        displayMapError(false);

        mapMenu.SetActive(!mapMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    /** Display's score screen. **/
    public void toggleScores()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        scoreDisplayed = !scoreDisplayed;

        scoreScreenUI.SetActive(!scoreScreenUI.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    public void initialiseMap()
    {
        Scene scene = SceneManager.GetActiveScene();

        mapText = GameObject.Find("HUD/MapScreen/MapScreenUI/Text").GetComponentsInChildren<Text>();

        foreach (Text mapText in mapText)
        {
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

    private void toggleHelp()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showHelp = !showHelp;

        helpDisplay.SetActive(!helpDisplay.activeInHierarchy);

        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;
    }

    public void toggleScroll()
    {

        timeSinceClicked = sw.ElapsedMilliseconds;
        if (timeSinceClicked < 600)
        {
            return;
        }
        if (Player.loggedIn)
        {
            saveGame();
        }
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;

        switch (characterCount)
        {
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

        sw.Start();

    }

    private IEnumerator toggleStoryScroll()
    {
        storyFlag = true;
        yield return new WaitForSeconds(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
        player.GetComponent<FirstPersonController>().enabled = false;

        switch (storyCount)
        {
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

    public void enterAdvisorsOpinion()
    {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        switch (characterCount)
        {
            case 1:
                advisorsOpinion1.SetActive(!advisorsOpinion1.activeInHierarchy);
                break;
            case 2:
                advisorsOpinion2.SetActive(!advisorsOpinion2.activeInHierarchy);
                break;
            case 3:
                advisorsOpinion3.SetActive(!advisorsOpinion3.activeInHierarchy);
                break;
            case 4:
                advisorsOpinion4.SetActive(!advisorsOpinion4.activeInHierarchy);
                break;
            case 5:
                advisorsOpinion5.SetActive(!advisorsOpinion5.activeInHierarchy);
                break;
            default:
                break;
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterEmpireMap()
    {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        switch (characterCount)
        {
            case 1:
                empireMap1.SetActive(!empireMap1.activeInHierarchy);
                break;
            case 2:
                empireMap2.SetActive(!empireMap2.activeInHierarchy);
                break;
            case 3:
                empireMap3.SetActive(!empireMap3.activeInHierarchy);
                break;
            case 4:
                empireMap4.SetActive(!empireMap4.activeInHierarchy);
                break;
            case 5:
                empireMap5.SetActive(!empireMap5.activeInHierarchy);
                break;
            default:
                break;
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterPeoplesOpinion()
    {
        setScrollDisplaysToFalse();
        //Player.updateAbilityPoints(1); 
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        switch (characterCount)
        {
            case 1:
                peoplesOpinion1.SetActive(!peoplesOpinion1.activeInHierarchy);
                break;
            case 2:
                peoplesOpinion2.SetActive(!peoplesOpinion2.activeInHierarchy);
                break;
            case 3:
                peoplesOpinion3.SetActive(!peoplesOpinion3.activeInHierarchy);
                break;
            case 4:
                peoplesOpinion4.SetActive(!peoplesOpinion4.activeInHierarchy);
                break;
            case 5:
                peoplesOpinion5.SetActive(!peoplesOpinion5.activeInHierarchy);
                break;
            default:
                break;
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterLieutOpinion()
    {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        switch (characterCount)
        {
            case 1:
                lieutOpinion1.SetActive(!lieutOpinion1.activeInHierarchy);
                break;
            case 2:
                lieutOpinion2.SetActive(!lieutOpinion2.activeInHierarchy);
                break;
            case 3:
                lieutOpinion3.SetActive(!lieutOpinion3.activeInHierarchy);
                break;
            case 4:
                lieutOpinion4.SetActive(!lieutOpinion4.activeInHierarchy);
                break;
            case 5:
                lieutOpinion5.SetActive(!lieutOpinion5.activeInHierarchy);
                break;
            default:
                break;
        }
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void enterCouncilOpinion()
    {
        setScrollDisplaysToFalse();
        self.updateAbilityPoints(1);
        Time.timeScale = Math.Abs(Time.timeScale - 1);

        switch (characterCount)
        {
            case 1:
                councilOpinion1.SetActive(!councilOpinion1.activeInHierarchy);
                break;
            case 2:
                councilOpinion2.SetActive(!councilOpinion2.activeInHierarchy);
                break;
            case 3:
                councilOpinion3.SetActive(!councilOpinion3.activeInHierarchy);
                break;
            case 4:
                councilOpinion4.SetActive(!councilOpinion4.activeInHierarchy);
                break;
            case 5:
                councilOpinion5.SetActive(!councilOpinion5.activeInHierarchy);
                break;
            default:
                break;
        }
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

    public void returntoGameFromInspect()
    {
        inspectMenu.SetActive(false);
        Cursor.visible = false;
        inspectDisplayed = false;
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


    public void returntoGameFromHelp()
    {
        helpDisplay.SetActive(false);
        Cursor.visible = false;
        showHelp = false;
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

    public void setScrollDisplaysToFalse()
    {
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

    public void setStoryDisplaysToFalse()
    {
        storyDisplay1.SetActive(false);
        storyDisplay2.SetActive(false);
        storyDisplay3.SetActive(false);
        storyDisplay4.SetActive(false);
        storyDisplay5.SetActive(false);
        storyDisplay6.SetActive(false);
    }

    public void setAdvisorsOpinionToFalse()
    {
        advisorsOpinion1.SetActive(false);
        advisorsOpinion2.SetActive(false);
        advisorsOpinion3.SetActive(false);
        advisorsOpinion4.SetActive(false);
        advisorsOpinion5.SetActive(false);
    }

    public void setEmpireOpinionToFalse()
    {
        empireMap1.SetActive(false);
        empireMap2.SetActive(false);
        empireMap3.SetActive(false);
        empireMap4.SetActive(false);
        empireMap5.SetActive(false);
    }

    public void setPeoplesOpinionToFalse()
    {
        peoplesOpinion1.SetActive(false);
        peoplesOpinion2.SetActive(false);
        peoplesOpinion3.SetActive(false);
        peoplesOpinion4.SetActive(false);
        peoplesOpinion5.SetActive(false);
    }

    public void setLieutOpinionToFalse()
    {
        lieutOpinion1.SetActive(false);
        lieutOpinion2.SetActive(false);
        lieutOpinion3.SetActive(false);
        lieutOpinion4.SetActive(false);
        lieutOpinion5.SetActive(false);
    }

    public void setCouncilOpinionToFalse()
    {
        councilOpinion1.SetActive(false);
        councilOpinion2.SetActive(false);
        councilOpinion3.SetActive(false);
        councilOpinion4.SetActive(false);
        councilOpinion5.SetActive(false);
    }

    public void setAbilityDisplaysToFalse()
    {
        setAdvisorsOpinionToFalse();
        setEmpireOpinionToFalse();
        setPeoplesOpinionToFalse(); 
        setLieutOpinionToFalse(); 
        setCouncilOpinionToFalse();      
        insufficientAbilityPoints.SetActive(false);

        // todo temp dom
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            return;
        }
        inspectMenu.SetActive(false);
    }

    // Returns true if the requested level is not the current, false otherwise. Used for map navigation.
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
                
            }
            else if (nextLevel > Player.latestScene) // go to next level - todo need to check if you've completed previous scrolls 
            // maybe charactercount > that levels max character count
            {
                Player.currentAbilityPoints = 3;
                Player.latestScene = nextLevel;
                Player.characterCount = 1;
                Player.latestCharacterCount = Player.characterCount;
            }
            // toggleMap();
        }
        else
        {
            displayMapError(true);
        }
        
        // save game every level change
        if (Player.loggedIn)
        {
            saveGame();
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

