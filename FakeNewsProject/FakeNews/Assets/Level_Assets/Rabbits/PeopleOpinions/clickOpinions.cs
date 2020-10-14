using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class clickOpinions : MonoBehaviour
{

    public GameObject player;
    public arrowsToPeople opinionsScript;
    public PopUpMenus pop; 
    public dialogue1 dialogue;
    public GameObject dial;
    private bool pressedBefore = false; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject == opinionsScript.characters[opinionsScript.characters.Count - 1].gameObject)
        {
            opinionsScript.endOpions(); 
        }
        if (opinionsScript.active == 1 && pressedBefore == false)
        {
            pressedBefore = true; 
            dial.gameObject.SetActive(true);

            pop.dialoguing = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<FirstPersonController>().enabled = false;

            TriggerDialogue();

            if (FindObjectOfType<dialogueManager>().finished == true)
            {
                EndPerson(); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<dialogueManager>().finished == true)
        {

            EndPerson();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue); 
    }

    private void EndPerson()
    {
        pop.dialoguing = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        dial.gameObject.SetActive(false);
        FindObjectOfType<dialogueManager>().finished = false;
        opinionsScript.nextPerson(); 


    }

}
