using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private int stages;

    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
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
}
