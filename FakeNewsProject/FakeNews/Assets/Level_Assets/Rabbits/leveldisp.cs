using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class leveldisp : MonoBehaviour
{
    private Scene scene;
    public Text text; 
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Regex.Match(scene.name, @"\d+").Value; 

    }
}
