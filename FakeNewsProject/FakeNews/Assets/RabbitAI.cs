//You are free to use this script in Free or Commercial projects
//sharpcoderblog.com @2019

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class RabbitAI : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rawSpeed = 100f;

    private bool isIdle = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isRunning = false; 


    private void Start()
    {

    }


    private void Update()
    {
        if (isIdle == false)
        {
            //print("first");
            StartCoroutine(wander());
        }
        if (isRotatingRight == true)
        {
           // print("second");
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);

        }
        if (isRotatingLeft == true)
        {
            //print("third");
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
        }
        if (isRunning == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator wander()
    {
        int rotateTime = Random.Range(1, 2);
        int rotateWeight = Random.Range(1, 4);
        int rotateLR = Random.Range(1, 3);
        int walkWeight = Random.Range(1, 4);
        int walkTime = Random.Range(1, 3);

        isIdle = true;

        yield return new WaitForSeconds(walkWeight);

        isRunning = true;
        yield return new WaitForSeconds(walkTime);
        isRunning = false;
        yield return new WaitForSeconds(rotateWeight);
        print(rotateLR);
        if (rotateLR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotateTime);
            isRotatingRight = false;
        }

        if (rotateLR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotateTime);
            isRotatingLeft = false;
        }

        isIdle = false;

    }


}