using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthCounter : MonoBehaviour
{
    Text health; 
    void Start () {
        health = GetComponent<Text> ();
    }

    void Update () {
        health.text = "Empire Strength: " + GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }
}