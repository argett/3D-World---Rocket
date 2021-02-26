using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRocket : MonoBehaviour
{
    public GameObject rocket;
    public GameObject[] coiffe;
    public GameObject[] carburant;
    public GameObject[] moteur;

    private bool stage_completed;
    private GameObject rocketPart_recentlyCreated;

    // Start is called before the first frame update
    void Start()
    {
        stage_completed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeObject(GameObject part)
    {
        float rocketPad_heigth = 3;
        float objSize;

        objSize = part.GetComponent<MeshRenderer>().bounds.size.y; // we take its eight
        rocket.GetComponent<Rocket>().addHeigth(objSize);

        rocket.transform.position += new Vector3(0, objSize, 0);

        Transform parent = rocket.transform.parent;

        switch (part.name)
        {
            case "Coiffe_1(Clone)":
                rocketPart_recentlyCreated = Instantiate(coiffe[0], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                break;
            case "Coiffe_2(Clone)":
                rocketPart_recentlyCreated = Instantiate(coiffe[1], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                break;
            case "Coiffe_3(Clone)":
                rocketPart_recentlyCreated = Instantiate(coiffe[2], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                break;
            case "Tank_1(Clone)":
                rocketPart_recentlyCreated = Instantiate(carburant[0], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                ChooseTag(rocketPart_recentlyCreated);
                rocket.GetComponent<Rocket>().addTank(rocketPart_recentlyCreated);
                stage_completed = false;
                break;
            case "Tank_2(Clone)":
                rocketPart_recentlyCreated = Instantiate(carburant[1], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                rocket.GetComponent<Rocket>().addTank(rocketPart_recentlyCreated);
                ChooseTag(rocketPart_recentlyCreated);
                stage_completed = false;
                break;
            case "Tank_3(Clone)":
                rocketPart_recentlyCreated = Instantiate(carburant[2], new Vector3(parent.position.x, objSize/2 + rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 8;
                rocket.GetComponent<Rocket>().addTank(rocketPart_recentlyCreated);
                ChooseTag(rocketPart_recentlyCreated);
                stage_completed = false;
                break;
            case "Engine_1(Clone)":
                rocketPart_recentlyCreated = Instantiate(moteur[0], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 12;
                ChooseTag(rocketPart_recentlyCreated);
                rocket.GetComponent<Rocket>().addEngines(rocketPart_recentlyCreated);
                stage_completed = true ;
                break;
            case "Engine_2(Clone)":
                rocketPart_recentlyCreated = Instantiate(moteur[1], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 12;
                rocket.GetComponent<Rocket>().addEngines(rocketPart_recentlyCreated);
                ChooseTag(rocketPart_recentlyCreated);
                stage_completed = true;
                break;
            case "Engine_3(Clone)":
                rocketPart_recentlyCreated = Instantiate(moteur[2], new Vector3(parent.position.x, rocketPad_heigth, parent.position.z), Quaternion.identity, rocket.transform);
                rocketPart_recentlyCreated.layer = 12;
                ChooseTag(rocketPart_recentlyCreated);
                rocket.GetComponent<Rocket>().addEngines(rocketPart_recentlyCreated);
                stage_completed = true;
                break;
            default:
                Debug.Log("Problem selecting a rocket part in script CreateRocke");
                break;
        }
        rocket.GetComponent<Rocket>().addParts(rocketPart_recentlyCreated);
    }

    public void ChooseTag(GameObject RocketPart)
    {
        if (rocket.GetComponent<Rocket>().getStage() == 1)
        {
            RocketPart.tag = "First_floor";

        }else if(rocket.GetComponent<Rocket>().getStage() == 2)
        {
            RocketPart.tag = "Second_floor";
        }else if (rocket.GetComponent<Rocket>().getStage() == 3)
        {
            RocketPart.tag = "Third_floor";
        }
    }
    public bool isStageCompleted()
    {
        return stage_completed;
    }

    public void stageCreation()
    {
        stage_completed = false;
    }
}
