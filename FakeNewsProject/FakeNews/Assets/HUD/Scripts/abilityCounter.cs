using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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