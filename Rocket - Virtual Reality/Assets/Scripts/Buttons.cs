using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject output;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add_stage()
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
                GameObject button = GameObject.FindGameObjectWithTag("GameController");
                button.GetComponent<PresentRocketParts>().nextStage();
            }
        }
    }

    public void Goto_lunch_pad()
    {
        GameObject rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        if (rocket.GetComponent<Rocket>().getFuelTransfered()>= 100 && rocket.GetComponent<Rocket>().getParts().Count %2 != 0 && rocket.GetComponent<Rocket>().getParts().Count != 1)
        {
            GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
            gameHandler.GetComponent<ChangeScene>().load_lunch_pad();
        }
        else
        {
            output.GetComponent<TextMesh>().text = "The rocket is not finished,\ndon't forget to fill the fuel\nand complete the rocket";
            output.GetComponent<TextMesh>().color = Color.red;
        }
        
    }

    public void Decollage(GameObject rocket)
    {
        rocket.GetComponent<TakeOff>().takeOff();
    }
}
