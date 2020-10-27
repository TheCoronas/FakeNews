using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenuControl : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
    }

    public void ButtonAbout()
    {
        // todo change to real scene name
        SceneManager.LoadScene("Scenes/Menus/intro1Scene 1");
    }

    public void ButtonNext()
    {
        // todo change to real scene name
        SceneManager.LoadScene("Scenes/Menus/intro2Scene");
    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene("Scenes/Menus/start_screen");
    }
}
