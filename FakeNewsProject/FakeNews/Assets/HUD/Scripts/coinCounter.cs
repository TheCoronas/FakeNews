using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Sets the count on screen. 
* Author: Madison Beare
*/
public class coinCounter : MonoBehaviour
{
    Text coins; 
    void Start () {
        coins = GetComponent<Text> ();
    }

    void Update () {
        coins.text = "Coins: " + Player.currentCoins;
    }
}