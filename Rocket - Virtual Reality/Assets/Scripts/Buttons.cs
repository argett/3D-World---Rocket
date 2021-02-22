using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject winglet;

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
        GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
        gameHandler.GetComponent<ChangeScene>().load_lunch_pad();
    }

    public void Place_winglet()
    {
        Instantiate(winglet, new Vector3(-10, 1.5f, 0), Quaternion.identity);
    }

    public void Decollage(GameObject rocket)
    {
        Debug.Log(rocket.name);
        rocket.GetComponent<Decolage>().decolage();
    }
}
