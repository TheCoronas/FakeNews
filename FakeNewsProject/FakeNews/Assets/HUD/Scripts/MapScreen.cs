using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MapScreen : MonoBehaviour
{
    public static bool mapDisplayed = false;
    public GameObject mapMenu;
    public GameObject player;
    
    void Update()
    {
        if (Input.GetKeyDown("m")) {
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
        
        if (mapDisplayed == true) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
        }
    }
    
    public void returntoGame()
    {
        mapMenu.SetActive(false);
        Cursor.visible = false;
        mapDisplayed = false;
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
