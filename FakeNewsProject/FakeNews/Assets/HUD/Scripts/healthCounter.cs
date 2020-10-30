using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Sets the count on screen. 
* Author: Madison Beare
*/
public class healthCounter : MonoBehaviour
{
    Text health; 
    void Start () {
        health = GetComponent<Text> ();
    }

    void Update() => health.text = "Health: " + Player.CurrentHealth;
}