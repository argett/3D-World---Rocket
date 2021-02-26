using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private int stages;    
    private int fuelTransfered; // cpt to know how much fuel there is in the rocket
    private List<GameObject> Tanks, Parts, Engines;
    private float heigth;

    // Start is called before the first frame update
    void Start()
    {
        fuelTransfered = 0;
        stages = 1;
        heigth = 0;
        Parts = new List<GameObject>(); // all parts of the rocket, [0] is the capsule
        Tanks = new List<GameObject>();
        Engines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStage()
    {
        stages++;
    }

    public int getStage()
    {
        return stages;
    }
    public void addFuelTransfered()
    {
        fuelTransfered++ ;
    }

    public int getFuelTransfered()
    {
        return fuelTransfered;
    }

    public void addTank(GameObject tank)
    {
        Tanks.Add(tank);
    }

    public List<GameObject> getTanks()
    {
        return Tanks;
    }

    public List<GameObject> getParts()
    {
        return Parts;
    }

    public void addParts(GameObject part)
    {
        Parts.Add(part);
    }
    public List<GameObject> getEngines()
    {
        return Engines;
    }

    public void addEngines(GameObject engine)
    {
        Engines.Add(engine);
    }

    public void addHeigth(float f)
    {
        heigth += f;
    }

    public float getHeigth()
    {
        return heigth;
    }


}
