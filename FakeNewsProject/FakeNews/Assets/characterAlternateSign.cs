using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAlternateSign : MonoBehaviour
{
    private GameObject[] possibleCharacters;
    private GameObject[] allIndicators = new GameObject[5];

    private Character characterChecking;
    // Start is called before the first frame update
    void Start()
    {
        int count = 0; 
        possibleCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject t1 in possibleCharacters) {
            foreach (Transform t in t1.transform)
            {
                if (t.name == "NPC_Indicator")
                {
                    allIndicators[count] = t.gameObject;
                    count += 1;
                }// Do something to child one
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject x in allIndicators)
        {
            x.gameObject.SetActive(false); 
        }
        foreach (GameObject characterCheckingList in possibleCharacters)
        {
            characterChecking = characterCheckingList.GetComponent<Character>();
            if (characterChecking.getCharacterOrder() == PopUpMenus.characterCount)
            {
                break;
            }
        }
        foreach (Transform t in characterChecking.transform)
        {
            if (t.name == "NPC_Indicator")
            {
                t.gameObject.SetActive(true);
            }// Do something to child one
        }
    }
}
