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
        while (carburant >0)
        {        
            //flame.transform.position += new Vector3(reactor.transform.position.x,reactor.transform.position.y -5, reactor.transform.position.z);
            reactor.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
            carburant--;

        }
        carburant = 100;
        StartCoroutine(Destroy_Flame());
        
    }

    private IEnumerator Destroy_Flame()
    {
        int s = 10;
        GameObject reactor = GameObject.FindGameObjectsWithTag("Rocket")[0];
        while (s >0)
        {
            // process
            Thread.Sleep(1000);
            s--;
            yield return null;
        }
        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);
    }
}
