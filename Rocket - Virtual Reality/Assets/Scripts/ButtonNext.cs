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
        GameObject rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        GameObject rocketPad = GameObject.FindGameObjectsWithTag("Rocket_pad")[0];

        // verify that the previous stage contain a tank+engine before adding a new stage
        if (rocketPad.GetComponent<CreateRocket>().isStageCompleted())
        {
            // share the info that a new stage is going to be created, to not apply the changes 2 times in a row
            rocketPad.GetComponent<CreateRocket>().stageCreation();

            if (rocket.GetComponent<Rocket>().getStage() < 3)
            {
                rocket.GetComponent<Rocket>().addStage();
                GameObject button = GameObject.FindGameObjectsWithTag("GameController")[0];
                button.GetComponent<PresentRocketParts>().nextStage();
            }
        }
    }
}
