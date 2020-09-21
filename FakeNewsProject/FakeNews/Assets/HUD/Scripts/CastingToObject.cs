using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingToObject : MonoBehaviour
{
    public static string selectedObject;
    public string internalObject;
    public RaycastHit theObject;
    RaycastHit hit;

    public static int check; 

    public static string nameof; 

    public Character x; 
    
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theObject)) 
        {
            selectedObject = theObject.transform.gameObject.name;
            internalObject = theObject.transform.gameObject.name;

            //x = (Character)theObject.transform.gameObject; 
            nameof = hit.transform.name; 
            check = hit.collider.GetComponent<Character>().characterOrder; 
            
            Debug.Log("here"); 
            // Debug.Log(check); 
        }
    }
}
