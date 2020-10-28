using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScrollPoints : MonoBehaviour
{
    private GameObject[] possibleCharacters;
    private Character characterChecking;
    // Start is called before the first frame update
    void Start()
    {

        possibleCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject characterCheckingList in possibleCharacters) {
            characterChecking = characterCheckingList.GetComponent<Character>(); 
            if (characterChecking.getCharacterOrder() == PopUpMenus.characterCount)
            {
                break;
            }
        }
        GetComponent<TMPro.TextMeshProUGUI>().text = 
            "Damage: -" + characterChecking.getScrollCoins() +" coins, -" + +characterChecking.getScrollDamage() + " health";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
