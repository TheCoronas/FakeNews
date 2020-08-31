using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PopUpMenus : MonoBehaviour
{
    public static bool mapDisplayed = false;
    public bool gamePaused = false;
    public GameObject mapMenu;
    public GameObject player;
    public GameObject pauseMenu;
    
    void Update()
    {
        if (Input.GetKeyDown("m") && gamePaused == false) {
            toggleMap();            
        }
        
        if (Input.GetButtonDown("Cancel") && mapDisplayed == false) {
            togglePause();            
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = mapDisplayed || gamePaused;
    }

    public void toggleMap()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        mapDisplayed = !mapDisplayed;
            
        mapMenu.SetActive(!mapMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled; 
    }

    private void togglePause()
    {
        Time.timeScale = Math.Abs(Time.timeScale - 1);
        gamePaused = !gamePaused;
    
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled; 
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
    
    
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        gamePaused = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
