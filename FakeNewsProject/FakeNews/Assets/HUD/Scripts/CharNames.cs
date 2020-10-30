﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Author: Troy Wright
 * Raycasting which determines when a character is being targetted.
 * Character name should be displayed in the HUD for each of 5
 * characters.
 */
public class CharNames : MonoBehaviour
{
    // Assigned to the 5 holders of scrolls in the level.
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;
    public GameObject char5;

    // Assigned to the text used to display the character names.
    public GameObject char1Name;
    public GameObject char2Name;
    public GameObject char3Name;
    public GameObject char4Name;
    public GameObject char5Name;

    // Record if the name is displayed.
    public bool displayChar1 = false;
    public bool displayChar2 = false;
    public bool displayChar3 = false;
    public bool displayChar4 = false;
    public bool displayChar5 = false;

    // Call function when count reaches a multiple of 4.
    public int count = 0;

    /* Update is called once per frame. */
    void Update()
    {
        if (count > 0)
        {
            // Cast ray every 4 frames.
            RaycastHit hit;
            if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider.gameObject.tag == "Character")
                {
                    toggleCharName(hit.collider.gameObject);
                } else
                {
                    disableNames();
                }
            }
        }
        count++;
    }
    
    /* Display name of targetted character. */
    public void toggleCharName(GameObject character)
    {
        //Time.timeScale = Math.Abs(Time.timeScale - 1);

        if (character.tag == "Character")
        {
            // Check which name to display.
            if (character == char1)
            {
                displayChar1 = true;
                char1Name.SetActive(true);
            }
            else if (character == char2)
            {
                displayChar2 = true;
                char2Name.SetActive(true);
            }
            else if (character == char3)
            {
                displayChar3 = true;
                char3Name.SetActive(true);
            }
            else if (character == char4)
            {
                displayChar4 = true;
                char4Name.SetActive(true);
            }
            else if (character == char5)
            {
                displayChar5 = true;
                char5Name.SetActive(true);
            }

        }
    }

    /** Disable Character names from appearing when not selected **/
    public void disableNames()
    {
        displayChar1 = false;
        char1Name.SetActive(false);
        displayChar2 = false;
        char2Name.SetActive(false);
        displayChar3 = false;
        char3Name.SetActive(false);
        displayChar4 = false;
        char4Name.SetActive(false);
        displayChar5 = false;
        char5Name.SetActive(false);

    }
}
