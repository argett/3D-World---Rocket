using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Decolage : MonoBehaviour
{
    public GameObject flame, theDisplay;

    private int carburant = 15;
    private float dureecombustion, Max;
    private GameObject rocket;


    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        Max = rocket.transform.position.y;
        theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocket.transform.position.y >= Max)
        {
            theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString() + "\nSpeed : " + rocket.GetComponent<Rigidbody>().velocity.y;
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
        print("Time.time :" + Time.time);
        print("dureecombustion :" + dureecombustion);

        GameObject reactor = GameObject.FindGameObjectWithTag("Reactor");

        Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
        flame.SetActive(true);

        rocket.GetComponent<Launch_Camera>().Creat_Cam();
        StartCoroutine(Forces());
    }

    private IEnumerator Forces()
    {
        while (dureecombustion > Time.time)
        {
            rocket.GetComponent<Rigidbody>().AddForce(transform.up * 10);
            yield return null;
        }

        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);
    }
}


