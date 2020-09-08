﻿using System;
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
            SceneManager.LoadScene(3);
        }
        
    }
    
    public void ButtonReturn()
    {
        // todo change to real scene name
        SceneManager.LoadScene(0);
    }
}
