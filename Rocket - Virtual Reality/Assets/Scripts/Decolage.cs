using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decolage : MonoBehaviour
{
    public int carburant = 5;
    private float dureecombustion;
    public GameObject rocket, flame;
    // Start is called before the first frame update
    void Start()
    {

        flame.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void decolage()
    {
        dureecombustion = Time.time + carburant;
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
            rocket.GetComponent<Rigidbody>().AddForce(transform.up * 10);
            yield return null;

        }

        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);

    }

    

}

    
