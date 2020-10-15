using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questPointer : MonoBehaviour
{
    public Image img;
    public Transform target;
    public GameObject player;
    public Text meter; 




    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY; 

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot((target.position - player.transform.position).normalized, player.transform.forward) < 0)
        {
            print("behind");
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX; 
            }
        }


        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y + 130, minY, maxY);

        img.transform.position = pos;
        meter.text = "Collect opinion in: \n" + Mathf.Round(Vector3.Distance(target.position, player.transform.position)/2).ToString();


    }


    public void changePoint(GameObject newObject)
    {
        target = newObject.transform; 
    }
}
