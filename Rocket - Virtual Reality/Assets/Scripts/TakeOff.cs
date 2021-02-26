using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOff : MonoBehaviour
{
    //We creat the flame for the rocket engine
    public GameObject flame;
    //We creat the text to display all the needed information (height, speed and your final score)
    public GameObject theDisplay;
    //We creat the rocket
    public GameObject rocket;
    //We creat two list for the fuel and the engine's power
    public List<int> carburants;
    public List<int> powers;

    //We creat the varable that will calculate the duration of the added force
    private float dureecombustion, Max, weight;
    //We creat the reactor variable to put the flame on the last engine
    private GameObject reactor;
    //Represent the number of stages
    public int stages;


    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        Max = rocket.transform.position.y;
        theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString();
        stages = rocket.GetComponent<Rocket>().getStage();
        carburants = rocket.GetComponent<Calculate_characteristics>().getCarburants();
        powers = rocket.GetComponent<Calculate_characteristics>().getPower();
        weight = rocket.GetComponent<Calculate_characteristics>().getWeight();
        reactor = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (rocket.transform.position.y >= Max) //We uptdate the height and the speed, but only when the height have change to avoid infinite value for the velocity
        {
            theDisplay.GetComponent<Text>().text = "Height : " + rocket.transform.position.y.ToString() + "\nSpeed : " + rocket.GetComponent<Rigidbody>().velocity.y;
            Max = rocket.transform.position.y;
            theDisplay.GetComponent<Text>().alignment = TextAnchor.UpperRight;
            theDisplay.GetComponent<Text>().fontSize = 10;
        }
        else if(rocket.GetComponent<Rigidbody>().velocity.y < 1)
        {
            if (Max < 100) //We put an arbitrary value to verify if the rocket took off or not
            {
                theDisplay.GetComponent<Text>().text = "Oops it seems that your rocket does not work well\ntry again !";
            }
            else
            {
                theDisplay.GetComponent<Text>().text = "Congratulations, your score is : \n" + Max + " Meters";
            }
            
            theDisplay.GetComponent<Text>().alignment = TextAnchor.MiddleCenter; //We put the text on the middle
            theDisplay.GetComponent<Text>().fontSize = 30; //We increase its size
        }
    }

    public void takeOff()
    {
        rocket.GetComponent<Rigidbody>().isKinematic = false;
        rocket.GetComponent<Rigidbody>().useGravity = true;
        Find_Reactor();
        Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
        flame.SetActive(true);
        rocket.GetComponent<Launch_Camera>().Creat_Cam();
        StartCoroutine(Forces());        
    }

    public void Remove_floor()
    {
        //We need to indentify witch floor to remove thanks to the varable stages and the tags.
        if (stages == 1)
        {
            GameObject.FindGameObjectsWithTag("First_floor")[0].transform.parent = null;
            GameObject.FindGameObjectsWithTag("First_floor")[1].transform.parent = null;
            weight -= GameObject.FindGameObjectsWithTag("First_floor")[0].GetComponent<Characteristics>().weight;
            weight -= GameObject.FindGameObjectsWithTag("First_floor")[1].GetComponent<Characteristics>().weight;
            //We change the rigidbody to make those parts fall
            GameObject.FindGameObjectsWithTag("First_floor")[0].AddComponent<Rigidbody>();
            GameObject.FindGameObjectsWithTag("First_floor")[1].AddComponent<Rigidbody>();

        }
        else if (stages == 2)
        {
            GameObject.FindGameObjectsWithTag("Second_floor")[0].transform.parent = null;
            GameObject.FindGameObjectsWithTag("Second_floor")[1].transform.parent = null;
            weight -= GameObject.FindGameObjectsWithTag("Second_floor")[0].GetComponent<Characteristics>().weight;
            weight -= GameObject.FindGameObjectsWithTag("Second_floor")[1].GetComponent<Characteristics>().weight;

            GameObject.FindGameObjectsWithTag("Second_floor")[0].AddComponent<Rigidbody>();
            GameObject.FindGameObjectsWithTag("Second_floor")[1].AddComponent<Rigidbody>();
        }
        else if (stages == 3)
        {
            GameObject.FindGameObjectsWithTag("Third_floor")[0].transform.parent = null;
            GameObject.FindGameObjectsWithTag("Third_floor")[1].transform.parent = null;
            weight -= GameObject.FindGameObjectsWithTag("Third_floor")[0].GetComponent<Characteristics>().weight;
            weight -= GameObject.FindGameObjectsWithTag("Third_floor")[1].GetComponent<Characteristics>().weight;

            GameObject.FindGameObjectsWithTag("Third_floor")[0].AddComponent<Rigidbody>();
            GameObject.FindGameObjectsWithTag("Third_floor")[1].AddComponent<Rigidbody>();
        }
        stages--;


    }
    public void Find_Reactor()
    {
        //Here we need to find the last reactor to instantiate the flame on it.
        GameObject temp;
        for (int i = 0; i < 2; i++)//There is only 2 parts by stage with the same tag, the reactor and the tank
        {
            if(stages == 1)
            {
                temp = GameObject.FindGameObjectsWithTag("First_floor")[i];
                if (temp.layer == 12)//Only the reactors have a layer equal to 12
                {
                    reactor = temp;
                }
            }else if (stages == 2)
            {
                temp = GameObject.FindGameObjectsWithTag("Second_floor")[i];
                if (temp.layer == 12)
                {
                    reactor = temp;
                }
            }else if (stages == 3)
            {
                temp = GameObject.FindGameObjectsWithTag("Third_floor")[i];
                if (temp.layer == 12)
                {
                    reactor = temp;
                }
            }
        }
        
    }


    private IEnumerator Forces()
    {
        //We change the fuel value in a time value
        dureecombustion = Time.timeSinceLevelLoad + carburants[stages - 1];
        while (dureecombustion > Time.time)
        {
            if(rocket.GetComponent<Rigidbody>().velocity.y > 500)
            {
                //We reduce the added force to avoid too high velocity
                rocket.GetComponent<Rigidbody>().AddForce(transform.up * (((powers[stages - 1] - weight))/7));
            }
            else
            {
                rocket.GetComponent<Rigidbody>().AddForce(transform.up * (powers[stages - 1] - weight));

            }
            yield return null;
        }

        Destroy(GameObject.FindGameObjectsWithTag("Fire")[0]);
        Remove_floor();
        if (stages != 0)//If there is no more stage, no need to instantiate a new flame and remove, find a reactor
        {
            Find_Reactor();
            Instantiate(flame, reactor.transform.position, Quaternion.Euler(-90f, 0f, 0f), reactor.transform);
            StartCoroutine(Forces());
        }
        else if(stages == 0)
        {
            //We increase the drag to reduce the leftover velocity
            rocket.GetComponent<Rigidbody>().drag = 0.1f;
            
        }
            
    }

    }


