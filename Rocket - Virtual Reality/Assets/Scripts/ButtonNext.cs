using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNext : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pushOnIt()
    {
        GameObject[] button = GameObject.FindGameObjectsWithTag("GameController");
        button[0].GetComponent<PresentRocketParts>().nextStage();
    }
}
