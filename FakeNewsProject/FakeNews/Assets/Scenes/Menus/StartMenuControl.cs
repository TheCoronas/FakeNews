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
        SceneManager.LoadScene("Scenes/Menus/login_screen 1");
    }
    
    public void ButtonSignUp()
    {
        // todo change to real scene name
        SceneManager.LoadScene("signup_screen");
    }
    
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
