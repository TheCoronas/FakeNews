using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class arrowsToPeople : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;
    public GameObject final;
    public int totalNumOpions = 2; 

    public PopUpMenus popup;
    public questPointer visualArrows; 

    public List<GameObject> characters = new List<GameObject>();

    public int active = 0; 
    public int currentPerson = 0;
    private bool done = false; 

    // Start is called before the first frame update
    void Start()
    {
        characters.Add(char1);
        characters.Add(char2);
        characters.Add(final); 


    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            GameObject symbol = GetSymbol(characters[currentPerson], "mini");
        }

    }

    private GameObject GetSymbol(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }
            

        }

        return null;
    }
    public void startOpions()
    {

        for (int i = 0; i < characters.Count(); i++)
        {
            GameObject symbol = GetSymbol(characters[i], "mini");

            symbol.SetActive(true);

        }

        popup.returnToGameFromScroll();
        popup.scrollClicked = false; 
        visualArrows.changePoint(characters[currentPerson]);
        visualArrows.gameObject.SetActive(true);
        active = 1; 

    }

    public void nextPerson()
    {
        currentPerson += 1; 
        visualArrows.changePoint(characters[currentPerson]); 
        
    }

    public void endOpions()
    {
        done = true; 
        for (int i = 0; i < characters.Count(); i++)
        {
            GameObject symbol = GetSymbol(characters[i], "mini");
            visualArrows.gameObject.SetActive(false);
            symbol.SetActive(false);

        }

    }

}