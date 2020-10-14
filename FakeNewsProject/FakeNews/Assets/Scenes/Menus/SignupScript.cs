using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEngine.EventSystems;

public class SignupScript : MonoBehaviour
{
    public string username;
    public string password;
    public GameObject usernameInputField;
    public GameObject passwordInputField;
    public GameObject textDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
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
            submitButton();   
        }
    }

    // change focus to next input 
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

     
    public void submitButton()
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
        
        // send to server
        WebClient client = new WebClient();
        byte[] response = client.UploadValues("https://corona.uqcloud.net/test/welcome/register", values);
        var result = Encoding.UTF8.GetString(response);
    
        Debug.Log(result);

        // server will respond with fail if username already exists
        if (result.Equals("fail"))
        {
            textDisplay.GetComponent<Text>().text = "Please try another username";
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "Successful! Please return and login.";
            // setting default values is handled by the webserver when you signup
        }
    }
    
    public void ButtonReturn()
    {
        SceneManager.LoadScene("Scenes/Menus/start_screen");
    }
}
