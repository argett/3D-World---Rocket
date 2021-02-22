using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Decolage : MonoBehaviour
{
    private int carburant = 15;
    private float dureecombustion, Max, speed = 0, oldTime, oldAltitude;
    public GameObject rocket, flame, theDisplay;
    // Start is called before the first frame update
    void Start()
    {
        Max = rocket.transform.position.y;
        theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString();
        flame.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Max!= rocket.transform.position.y)
        {
            speed = 
            oldAltitude = rocket.transform.position.y;
            oldTime = Time.time;
        }
        
        if (rocket.transform.position.y >= Max)
        {
            theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString() + " Speed : " + rocket.GetComponent<Rigidbody>().velocity.y;
            Max = rocket.transform.position.y;
        }
        else
        {
            theDisplay.GetComponent<Text>().text = "Max Height : " + Max;
        }

        
    }

    public void decolage()
    {
        dureecombustion = Time.timeSinceLevelLoad + carburant;
        print("carburant :" + carburant);
        print("Time.time :"+Time.time);
        print("dureecombustion :"+dureecombustion);
        GameObject reactor = GameObject.FindGameObjectsWithTag("Reactor")[0];
        rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
        rocket.GetComponent<Launch_Camera>().Creat_Cam();
        StartCoroutine(Forces());

    }

    private IEnumerator Forces()
    {
        rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        while (dureecombustion > Time.time)
        {
            rocket.GetComponent<Rigidbody>().AddForce(transform.up * 3);
            
            yield return null;

        }

        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);

    }
   
    



}

    
