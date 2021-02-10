﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRocket : MonoBehaviour
{
    public GameObject rocket;
    public GameObject[] coiffe;
    public GameObject[] carburant;
    public GameObject[] moteur;

    private float size;

    // Start is called before the first frame update
    void Start()
    {
        size = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeObject(GameObject part)
    {
        float rocketPad_heigth = 3;
        float objSize;

        /* get the size of the object to pushup the entire rocket of the same value to place the new part under it */
        if (part.name == "Engine_1(Clone)" || part.name == "Engine_2(Clone)" || part.name == "Engine_3(Clone)")
            objSize = part.GetComponent<MeshRenderer>().bounds.size.x;
        else
            objSize = part.GetComponent<MeshRenderer>().bounds.size.y; // we take its eight

        rocket.transform.position += new Vector3(0, objSize, 0);
        size += objSize;
        /* ------------------------- */

        Transform parent = rocket.transform.parent;

        switch (part.name)
        {
            case "Coiffe_1(Clone)":
                Instantiate(coiffe[0], new Vector3(parent.position.x, objSize + rocketPad_heigth, parent.position.z), Quaternion.Euler(0, 0f, 0f), rocket.transform);
                break;
            case "Coiffe_2(Clone)":
                Instantiate(coiffe[1], new Vector3(parent.position.x, objSize + rocketPad_heigth, parent.position.z), Quaternion.Euler(0, 0f, 0f), rocket.transform);
                break;
            case "Coiffe_3(Clone)":
                Instantiate(coiffe[2], new Vector3(parent.position.x, objSize + rocketPad_heigth, parent.position.z), Quaternion.Euler(0, 0f, 0f), rocket.transform);
                break;
            case "Tank_1(Clone)":
                Instantiate(carburant[0], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 0f), rocket.transform);
                break;
            case "Tank_2(Clone)":
                Instantiate(carburant[1], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 0f), rocket.transform);
                break;
            case "Tank_3(Clone)":
                Instantiate(carburant[2], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 0f), rocket.transform);
                break;
            case "Engine_1(Clone)":
                Instantiate(moteur[0], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 90f), rocket.transform);
                break;
            case "Engine_2(Clone)":
                Instantiate(moteur[1], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 90f), rocket.transform);
                break;
            case "Engine_3(Clone)":
                Instantiate(moteur[2], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.Euler(0f, 0f, 90f), rocket.transform);
                break;
            default:
                Debug.Log("Problem selecting a rocket part in script CreateRocke");
                break;
        }        
    }
}
