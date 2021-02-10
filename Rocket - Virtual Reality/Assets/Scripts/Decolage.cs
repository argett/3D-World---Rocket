using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Decolage : MonoBehaviour
{
    public int carburant = 100;
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

        GameObject reactor = GameObject.FindGameObjectsWithTag("Reactor")[0];
        rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
        rocket.GetComponent<Launch_Camera>().Creat_Cam();
        StartCoroutine(Destroy_Flame());
        
    }

    private IEnumerator Destroy_Flame()
    {
        GameObject reactor = GameObject.FindGameObjectsWithTag("Reactor")[0];
        while (carburant > 0)
        {
            Thread.Sleep(1000);
            reactor.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            carburant--;
            yield return null;

        }
        carburant = 100;
        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);
    }
}
