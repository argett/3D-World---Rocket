using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRocket : MonoBehaviour
{
    public GameObject coiffe;
    public GameObject carburant;
    public GameObject moteur;

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
        size += part.transform.localScale.y;

        if (part.name == "carburant(Clone)")
            Instantiate(carburant, new Vector3(10,0.1f,10), Quaternion.Euler(0f, 0f, 0f), this.transform);
        else if (part.name == "coiffe(Clone)")
            Instantiate(coiffe, new Vector3(10, 0.1f, 10), Quaternion.Euler(0f, 0f, 0f), this.transform);
        else
            Instantiate(moteur, new Vector3(10, 0.1f, 10), Quaternion.Euler(0f, 0f, 0f), this.transform);

        this.transform.position += new Vector3(0, part.transform.localScale.y, 0);
    }
}
