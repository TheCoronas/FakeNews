using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{

    public GameObject ui;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open()
    {

         Time.timeScale = 0f;
        
    }
    public void Close(){

        Time.timeScale = 1f;
    }

}