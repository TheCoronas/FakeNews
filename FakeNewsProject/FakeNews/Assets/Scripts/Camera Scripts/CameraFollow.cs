using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject _text;
    public TextMesh _startText;

    // Use this for initialization
    void Start()
    {
        if (_text == null) _text = GameObject.Find("StartText");
        if (_startText == null) _startText = GameObject.Find("StartText").GetComponent<TextMesh>();
        _text.SetActive(true);
        _text.transform.Rotate(90f, 90f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        print("Text position is: " + _text.transform.position); 
        if (_text.activeSelf)
        {
            var camPos = Camera.main.transform.position + Camera.main.transform.forward;
            _text.transform.position = camPos;
            _text.transform.localScale = Vector3.one * 0.025f;
        }

        else
        {
            Debug.Log("deactive _startText");
        }

    }
}