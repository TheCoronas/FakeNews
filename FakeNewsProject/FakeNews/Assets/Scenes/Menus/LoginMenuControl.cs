using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine.EventSystems;

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

    void Update()
    {
        // change focus on tab press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            setNextInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ButtonLogin();
        }
    }

    // changes cursor to next input field
    private void setNextInputField()
    {
        EventSystem system = EventSystem.current;
        try
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            next.Select();
        }
        catch (Exception e)
        {
        }
    }
    
   
    
    public void ButtonLogin()
    {
        username = usernameInputField.GetComponent<InputField>().text;
        password = passwordInputField.GetComponent<InputField>().text;

        // calculate hash
        byte[] passBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = new SHA1Managed().ComputeHash(passBytes);
        var sb = new StringBuilder(hashBytes.Length * 2);

        // make hash readable
        foreach (byte b in hashBytes) {
            sb.Append(b.ToString("x2"));
        }

        string hash = sb.ToString();

        var values = new NameValueCollection();
        values["username"] = username;
        values["password"] = hash;
        
        // temp credentials
        // username: smith
        // password: password
       
        // send to server
        WebClient client = new WebClient();
        byte[] response = client.UploadValues("https://corona.uqcloud.net/test/", values);
        var result = Encoding.UTF8.GetString(response);
    
        Debug.Log(result);

        if (result.Equals("false")) {
            // todo change to real scene name
            textDisplay.GetComponent<Text>().text = "Incorrect Username or Password!";
        } else if (result.Equals("")) {
            textDisplay.GetComponent<Text>().text = "Please enter username and password!";
        }else {
            SceneManager.LoadScene("Scenes/Level 1");
        }
    }
    
    public void ButtonReturn()
    {
        // todo change to real scene name
        SceneManager.LoadScene("Scenes/Menus/start_screen");
    }
}
