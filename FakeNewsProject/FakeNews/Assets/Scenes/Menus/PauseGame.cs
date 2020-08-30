using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject player;
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
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
        
//        if (gamePaused == true ) {
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//        } else {
//            Cursor.visible = false;
//        }
    }
            
        
    public void returntoGame()
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
    }
        
}
