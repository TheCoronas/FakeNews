﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu1 : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused; 

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused; 
        }

        if (isPaused)
        {
            ActivateMenu(); 
        } else
        {
            DeactivateMenu(); 
        }
        
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false); 
    }    
}
