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
    public int currentOp; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject == opinionsScript.characters[opinionsScript.characters.Count - 1].gameObject || currentOp != PopUpMenus.storyCount)
        {
            opinionsScript.endOpions(); 
        }
        if (opinionsScript.active == 1 && pressedBefore == false && currentOp == PopUpMenus.storyCount)
        {
            print(currentOp + "currentOP");
            print(PopUpMenus.storyCount + "storyCount popup"); 
            pressedBefore = true; 
            dial.gameObject.SetActive(true);

            pop.dialoguing = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<FirstPersonController>().enabled = false;

            TriggerDialogue();

            if (FindObjectOfType<dialogueManager>().finished == true)
            {
                EndPerson(opinionsScript); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<dialogueManager>().finished == true && currentOp == PopUpMenus.storyCount)
        {

            EndPerson(opinionsScript);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue); 
    }

    private void EndPerson(arrowsToPeople opinionsScript)
    {
        print("ending person");
        dial.gameObject.SetActive(false);
        pop.dialoguing = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        
        FindObjectOfType<dialogueManager>().finished = false;
        print("yes yes yes");
        print(opinionsScript);
        if (currentOp == PopUpMenus.storyCount)
        {
            opinionsScript.nextPerson();
        }


    }

}
