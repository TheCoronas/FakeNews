using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npclvl3 : MonoBehaviour
{

    public Transform Player;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;
    int played = 0; 

    public AudioSource audioSource;
    public ButtonFunction b1;



    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        else
        {
            b1.SprintSlide();
            MoveSpeed = 0; 
            if (played == 0) {
            audioSource.Play();
            played = 1;
        }
        }
    }
}
