using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginMenuControl : MonoBehaviour
{
    public string username;
    public string password;
    public GameObject usernameInputField;
    public GameObject passwordInputField;
    public GameObject textDisplay;
    
    public void StoreName()
    {
        textDisplay.GetComponent<Text>().text = "Welcome " + username + " to the game.";
    }
    
    public void ButtonLogin()
    {
        username = usernameInputField.GetComponent<Text>().text;
        password = passwordInputField.GetComponent<Text>().text;
//        textDisplay.GetComponent<Text>().text = "Incorrect Username or Password!";
        if (username == "" && password == "") {
            SceneManager.LoadScene(1);
        } else {
            textDisplay.GetComponent<Text>().text = "Incorrect Username or Password!";
        }
        
    }
    
    public void ButtonReturn()
    {
        SceneManager.LoadScene(0);
    }
}
