using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decolage : MonoBehaviour
{
    public int carburant = 10;
    public GameObject flame;
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
        Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
        while (carburant >0)
        {        
            //flame.transform.position += new Vector3(reactor.transform.position.x,reactor.transform.position.y -5, reactor.transform.position.z);
            reactor.GetComponent<Rigidbody>().AddForce(transform.up * 100);
            carburant--;

        }
        carburant = 10;
        Destroy(flame);
    }

}
