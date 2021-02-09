using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject player;

    private float speed = 100;
    private GameObject halo = null;
    private GameObject rocket_build;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] HALO = GameObject.FindGameObjectsWithTag("Halo");
        Debug.Log("l = " + HALO.Length);
        halo = HALO[0];
        halo.SetActive(false);

        GameObject[] ROCKET = GameObject.FindGameObjectsWithTag("Rocket");
        rocket_build = ROCKET[0];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
        {
            //check if the object is a rocket part or not
            if (hit.collider.gameObject.layer == 8)
            {
                halo.transform.position = hit.collider.gameObject.transform.position;
                halo.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    rocket_build.GetComponent<CreateRocket>().placeObject(hit.collider.gameObject);
                    // change the items
                    GameObject[] button = GameObject.FindGameObjectsWithTag("GameController");
                    button[0].GetComponent<PresentRocketParts>().pieceSelected();
                }
            }
            else if(hit.collider.gameObject.tag == "Button")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<ButtonNext>().pushOnIt();
                }
            }
            else
                halo.SetActive(false);
        }
        else
            halo.SetActive(false);
    }
}
