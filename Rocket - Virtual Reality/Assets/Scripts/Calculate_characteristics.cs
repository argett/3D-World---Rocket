using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate_characteristics : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rocket;
    private List<GameObject> Tanks, Parts, Engines;//We creat lists for the parts, tanks and engines that help us to calcule all the value needed to calcule the power of launch and the duration of it
    private int stages;//The numbers of stages
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        Tanks = rocket.GetComponent<Rocket>().getTanks();
        Parts = rocket.GetComponent<Rocket>().getParts();
        Engines = rocket.GetComponent<Rocket>().getEngines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> getCarburants()
    {
        //We calculate a list of fuel for every tanks
        stages = rocket.GetComponent<Rocket>().getStage();
        List<int> carburants = new List<int>();
        for (int i = 0; i < stages;i++)
        {
            carburants.Add(Tanks[i].GetComponent<Characteristics>().fuel);
        }
        
        return carburants;
    }

    public float getWeight()
    {
        //We calculate the total weight of the rocket
        stages = rocket.GetComponent<Rocket>().getStage();
        float weight = 0;
        for (int i = 0; i < (stages*2)+1; i++)
        {
            weight += Parts[i].GetComponent<Characteristics>().weight;
        }
        return weight;
    }

    public List<int> getPower()
    {
        //We calcule a list of power for every engine
        stages = rocket.GetComponent<Rocket>().getStage();
        List<int> powers = new List<int>();
        for (int i = 0; i < stages; i++)
        {
            powers.Add(Engines[i].GetComponent<Characteristics>().power);
        }

        return powers;
    }

}
