using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Sets the count on screen. 
* Author: Madison Beare
*/
public class abilityCounter : MonoBehaviour
{
    Text abilityPoints; 
    void Start () {
        abilityPoints = GetComponent<Text> ();
    }

    void Update () {
        abilityPoints.text = "Ability Points: " + Player.currentAbilityPoints;
    }
}