using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnSettingsMenu : MonoBehaviour
{

    public GameObject settingsScreen;
    public GameObject menuScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickedSettings()
    {
        menuScreen.SetActive(false);
        settingsScreen.SetActive(true); 
    }

    public void clickedBack()
    {
        menuScreen.SetActive(true);
        settingsScreen.SetActive(false);

    }
}
