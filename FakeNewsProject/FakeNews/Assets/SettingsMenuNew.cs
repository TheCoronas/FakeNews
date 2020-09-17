using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuNew : MonoBehaviour
{


    public GameObject menuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Open();
        }
    }

    public void Open()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Close()
    {
        menuCanvas.SetActive(false);

        Time.timeScale = 1f;
    }
}
