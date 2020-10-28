using UnityEngine;
using System.Collections;

public class TextTimer : MonoBehaviour
{
    public float time = 2; //Seconds to read the text
     

    void OnBecomeVisible()
    {
        print("AAA");
        Destroy(transform.parent.gameObject, time);
        print("BBB");
    }
    void Start()
    {
        print("AAA");
        Destroy(transform.parent.gameObject, time);
        print("BBB");
    }
}