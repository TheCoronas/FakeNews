using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControl : MonoBehaviour
{
    public void ButtonLogin()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ButtonSignUp()
    {
        SceneManager.LoadScene(3);
    }
    
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
