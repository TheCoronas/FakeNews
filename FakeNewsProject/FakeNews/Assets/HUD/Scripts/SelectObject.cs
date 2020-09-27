using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

// Please ignore this file for now, I'm still working on it. - JH
public class SelectObject : MonoBehaviour
{
    public GameObject selectedObject;

    public static bool scrollClicked = false;
    public GameObject scrollPopUp;
    public GameObject player;

    public static int currentCharacter; 
    public static int scrollAbilityPoints; 
    public static int scrollDamage; 
    public static int scrollCoins; 

    
    //void Update()
    //{
    //    //if (lookingAtObject == true)
    //    //{
    //    //    //selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
    //    //}
    //}
    
    void OnMouseDown()
    {
        scrollClicked = true;
        // To get the name of the character youve clicked on
        //Debug.Log(gameObject.name);
        currentCharacter = gameObject.GetComponent<Character>().getCharacterOrder();
        //currentScroll = gameObject.GetComponent<SelectedObject>().scrollPopUp();
        scrollDamage = gameObject.GetComponent<Character>().getScrollDamage();
        scrollAbilityPoints = gameObject.GetComponent<Character>().getScrollAbilityPoints(); 
        scrollCoins = gameObject.GetComponent<Character>().getScrollCoins();
        // To get their character order
        //Debug.Log(gameObject.GetComponent<Character>().getCharacterOrder());

    }
    
    void OnMouseUp()
    {
        scrollClicked = false;
        
    }
    
    public void returntoGameFromScroll()
    {
        scrollClicked = false;
    }

}
