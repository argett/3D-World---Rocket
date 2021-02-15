using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject player;
    public GameObject caracteristic_rocket_part_prefab;

    private GameObject rocket_build;
    private GameObject halo = null;
    private GameObject caracteristic_rocket_part;

    // Start is called before the first frame update
    void Start()
    {
        rocket_build = GameObject.FindGameObjectsWithTag("Rocket_pad")[0];
        halo = GameObject.FindGameObjectsWithTag("Halo")[0];
        halo.SetActive(false);
        caracteristic_rocket_part = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
        {
            
            if (hit.collider.gameObject.layer == 8) //layer Rocket part
            {


                halo.transform.position = hit.collider.gameObject.transform.position;
                halo.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    choseObject(hit.collider);
                }
                displayCaracteristics(hit.collider);
            }
            else if (hit.collider.gameObject.layer == 9) //layer button, UI
            {
                if (hit.collider.gameObject.tag == "Button")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        checkButtonType(hit.collider);
                    }
                }
            }
            else
            {
                halo.SetActive(false);
                if(caracteristic_rocket_part != null)
                {
                    Destroy(caracteristic_rocket_part);
                    caracteristic_rocket_part = null;
                }
            }
        }
        else
        {
            halo.SetActive(false);
            if (caracteristic_rocket_part != null)
            {
                Destroy(caracteristic_rocket_part);
                caracteristic_rocket_part = null;
            }
        }
    }

    private void displayCaracteristics(Collider obj)
    {
        if(caracteristic_rocket_part == null)
        {
            Vector3 parentPos = obj.gameObject.transform.position;
            caracteristic_rocket_part = Instantiate(caracteristic_rocket_part_prefab, new Vector3(parentPos.x, parentPos.y, parentPos.z), Quaternion.identity);
        }

        Caracteristics rocketPart_info = obj.GetComponent<Caracteristics>();
        int fuel = rocketPart_info.fuel;
        int weight = rocketPart_info.weight;
        int power = rocketPart_info.power;
        int crew = rocketPart_info.crew;

        string caract = "";
        if (fuel != 0)
            caract += "Total amount of fuel = " + fuel + "\n";
        if (weight != 0)
            caract += "Total weight of the part = " + weight + "\n";
        if (power != 0)
            caract += "Total power in Newton = " + power + "\n";
        if (crew != 0)
            caract += "Maximum crew = " + crew + "\n";

        caracteristic_rocket_part.GetComponent<TextMesh>().text = caract;
    }

    private void choseObject(Collider obj)
    {
        rocket_build.GetComponent<CreateRocket>().placeObject(obj.gameObject);
        // change the items
        GameObject button = GameObject.FindGameObjectsWithTag("GameController")[0];
        button.GetComponent<PresentRocketParts>().pieceSelected();
    }

    private void checkButtonType(Collider obj)
    {
        switch (obj.name)
        {
            case "Add_stage":
                obj.gameObject.GetComponent<Buttons>().Add_stage();
                break;
            case "Goto_lunch_pad":
                obj.gameObject.GetComponent<Buttons>().Goto_lunch_pad();
                break;
        }
    }
}
