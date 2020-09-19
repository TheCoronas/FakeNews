using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PopUpMenus : MonoBehaviour
{
    public bool mapDisplayed = false;
    public bool gamePaused = false;
    public bool scrollClicked;
    public bool showScroll = false;
    public bool displayGameOver = false;

    public bool displayIncorrectChar = false; 
    public GameObject mapMenu;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject scrollDisplay;
    public GameObject gameOverMenu;

    public GameObject incorrectCharacter; 

    public int currentCharacter; 
    
    public int currentCharacterCount; 
     void Start()
    {
        currentCharacter = 1; 
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
        currentCharacterCount = GameObject.FindWithTag("Character").GetComponent<Character>().characterOrder;
        Debug.Log(currentCharacter); 
        Debug.Log(currentCharacterCount); 
        //currentCharacterCount = SelectObject.currentCharacterCount; 
        if (scrollClicked == true && showScroll == false && mapDisplayed == false && gamePaused == false && displayGameOver == false) {
        Debug.Log(currentCharacter); 
        Debug.Log(currentCharacterCount); 
            if (currentCharacterCount != currentCharacter) {
            //{
                //toggleScroll(); 
                incorrectCharacterCall(); 
            }
            else {
                toggleScroll();
                //incorrectCharacterCall(); 
            }
        }
        //}
        
        // Cursor needs to be dealt with like this, or else it jams. 
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
            
        mapMenu.SetActive(!mapMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    private void togglePause()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        gamePaused = !gamePaused;
    
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false; 
    }
    
    private void toggleScroll()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        showScroll = !showScroll;
    
        scrollDisplay.SetActive(!scrollDisplay.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    private void enterGameOver()
    {
        displayGameOver = true;
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
        scrollDisplay.SetActive(false);
        incorrectCharacter.SetActive(false);
        Cursor.visible = false;
        showScroll = false;
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
}
