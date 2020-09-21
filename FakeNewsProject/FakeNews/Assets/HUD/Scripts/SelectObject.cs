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
    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    public static bool scrollClicked = false;
    public GameObject scrollPopUp;
    public GameObject player;

    public static int currentCharacter; 
    public static int scrollAbilityPoints; 
    public static int scrollDamage; 
    public static int scrollCoins; 

    
    void Update()
    {
        if (lookingAtObject == true)
        {
            selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
        }
    }
    
    void OnMouseDown()
    {
        scrollClicked = true;
        // To get the name of the character youve clicked on
        //Debug.Log(gameObject.name);
        currentCharacter = gameObject.GetComponent<Character>().getCharacterOrder();
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

    

//    void OnMouseOver()
//    {
//        selectedObject = GameObject.Find(CastingToObject.selectedObject);
//        lookingAtObject = true;
//        
//        if(startedFlashing == false) 
//        {
//            startedFlashing = true;
//            StartCoroutine(FlashObject());
//        }
//    }
//    
//    void OnMouseExit()
//    {
//        startedFlashing = false;
//        lookingAtObject = false;
//        
//        StopCoroutine(FlashObject());
//        selectedObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
//    }
    
//    IEnumerator FlashObject()
//    {
//        while(lookingAtObject == true)
//        {
//            yield return new WaitForSeconds(0.05f);
//            if (flashingIn == true)
//            {
//                if (blueCol <= 30) 
//                {
//                    flashingIn = false;
//                }
//                else 
//                {
//                    blueCol -= 25;
//                    greenCol -= 1;
//                }
//            }
//            
//            if (flashingIn == false){    
//                if (blueCol >= 250) 
//                {
//                    flashingIn = true;
//                }
//                else 
//                {
//                    blueCol += 25;
//                    greenCol += 1;
//                }
//            }
//        }
//    }
}
