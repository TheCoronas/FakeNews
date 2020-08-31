using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControl : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
    }

    public void ButtonLogin()
    {
        // todo change to real scene name
        SceneManager.LoadScene(2);
    }
    
    public void ButtonSignUp()
    {
        // todo change to real scene name
        SceneManager.LoadScene(3);
    }
    
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
