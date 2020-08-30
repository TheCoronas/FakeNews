using System.Collections;
using System.Collections.Generic;
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
            if (mapDisplayed == false)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                mapDisplayed = true;
            
                mapMenu.SetActive(true);
                player.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                mapMenu.SetActive(false);
                Cursor.visible = false;
                mapDisplayed = false;
                Time.timeScale = 1;
                player.GetComponent<FirstPersonController>().enabled = true;
            }
            
        }
        
        if (Input.GetButtonDown("Cancel") && mapDisplayed == false) {
            if (gamePaused == false)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                gamePaused = true;
            
                pauseMenu.SetActive(true);
                player.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1;
                player.GetComponent<FirstPersonController>().enabled = true;
            }
            
        }
        
        
        if (mapDisplayed == true || gamePaused == true) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
        }
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
