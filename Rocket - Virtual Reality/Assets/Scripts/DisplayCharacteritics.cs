using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacteritics : MonoBehaviour
{
    private GameObject rocket;
    private GameObject Capsule;
    private List<GameObject> Tanks, Engines;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        Tanks = new List<GameObject>();
        Engines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         S1 = stage 1, S2 = stage 2, S3 = stage 3
         in real life, the upper stage has the number the higher & the lower stage has the number the lower
         but in the program, for order of creation reasons, this is the inverse
         So because we got the inverse, we must inverse to get what is normal IRL
            We inverse at the end of the computation, just before displaying
        */

        int nbStage = rocket.GetComponent<Rocket>().getStage();
        int weightS1 = 0;
        int weightS2 = 0;
        int weightS3 = 0;
        int powerS1 = 0;
        int powerS2 = 0;
        int powerS3 = 0;
        int fuelS1 = 0;
        int fuelS2 = 0;
        int fuelS3 = 0;

        string txt = "";

        Tanks = rocket.GetComponent<Rocket>().getTanks();
        Engines = rocket.GetComponent<Rocket>().getEngines();


        if (Engines.Count != 0) // if there is at least 1 stage
        {
            Capsule = rocket.GetComponent<Rocket>().getParts()[0];

            if (nbStage >= 1 && Engines.Count >= 1)
            {
                weightS1 = Capsule.GetComponent<Characteristics>().weight + Tanks[0].GetComponent<Characteristics>().weight + Engines[0].GetComponent<Characteristics>().weight;
                powerS1 = Engines[0].GetComponent<Characteristics>().power;
                fuelS1 = Tanks[0].GetComponent<Characteristics>().fuel;
                txt += "First stage      : Weigth = " + weightS1 + " T, Power = " + powerS1 + " kN, Fuel = " + fuelS1 + " kL";
            }
            if (nbStage >= 2 && Engines.Count >= 2)
            {
                weightS2 = Tanks[1].GetComponent<Characteristics>().weight + Engines[1].GetComponent<Characteristics>().weight;
                powerS2 = Engines[1].GetComponent<Characteristics>().power;
                fuelS2 = Tanks[1].GetComponent<Characteristics>().fuel;
                txt += "\nSecond stage : Weigth = " + weightS2 + "T , Power = " + powerS2 + " kN, Fuel = " + fuelS2 + " kL";
            }
            if (nbStage == 3 && Engines.Count == 3)
            {
                weightS3 = Tanks[2].GetComponent<Characteristics>().weight + Engines[2].GetComponent<Characteristics>().weight;
                powerS3 = Engines[2].GetComponent<Characteristics>().power;
                fuelS3 = Tanks[2].GetComponent<Characteristics>().fuel;
                txt += "\nThird stage     : Weigth = " + weightS3 + "T , Power = " + powerS3 + " kN, Fuel = " + fuelS3 + " kL";
            }

            txt += "\n\nTotal Weight = " + (weightS1 + weightS2 + weightS3) + " T";
            txt += "\nTotal Fuel     = " + (fuelS1 + fuelS2 + fuelS3) + " kL";
            txt += "\nTotal Power  = " + (powerS1 + powerS2 + powerS3) + " kN";
            txt += "\nTotal Crew    = " + Capsule.GetComponent<Characteristics>().crew;

            txt += "\n\nThe delta-V is the maximum change of velocity of\nthe vehicule with no external forces (friction, gravity, heat...)";
            txt += "\nThe delta-V of your rocket is " + (((float)powerS1/ (float)weightS1 )* fuelS1 + ((float)powerS2 / (float)weightS1)* fuelS2 + ((float)powerS3 / (float)weightS1)* fuelS3);
        }
        else
        {
            txt = "Build a first stage";
        }
        
        this.GetComponent<TextMesh>().text = txt;

    }
}
