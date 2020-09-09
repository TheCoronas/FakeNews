using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCounter : MonoBehaviour
{
    Text coins; 
    void Start () {
        coins = GetComponent<Text> ();
    }

    void Update () {
        coins.text = "Coins: " + GameObject.Find("Player").GetComponent<Player>().currentCoins;
    }
}