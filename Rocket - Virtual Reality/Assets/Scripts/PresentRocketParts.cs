using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentRocketParts : MonoBehaviour
{
    public GameObject[] coiffe;
    public GameObject[] carburant;
    public GameObject[] moteur;

    private GameObject rocketParts_displayed_one;
    private GameObject rocketParts_displayed_two;
    private GameObject rocketParts_displayed_three;

    private bool another_stage; // 1 stage = (1:n)tank + 1 engine
    private bool isTankTurn;
    private bool isEngineTurn;

    // Start is called before the first frame update
    void Start()
    {
        rocketParts_displayed_one = Instantiate(coiffe[0], new Vector3(6, 4.3f, -1.2f), Quaternion.Euler(0f, 0f, -180f));
        rocketParts_displayed_two = Instantiate(coiffe[1], new Vector3(6, 4.3f, 0), Quaternion.Euler(0f, 0f, -180f));
        rocketParts_displayed_three = Instantiate(coiffe[2], new Vector3(6, 4.3f, 1.2f), Quaternion.Euler(0f, 0f, -180f));

        // to set at least 1 stage
        another_stage = true;
        isTankTurn = true;
        isEngineTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pieceSelected()
    {
        if (isTankTurn)
            placeTank();
        else if (isEngineTurn)
            placeEngine();
        else
        {
            Destroy(rocketParts_displayed_one);
            Destroy(rocketParts_displayed_two);
            Destroy(rocketParts_displayed_three);
        }
    }

    public void nextStage()
    {
        isTankTurn = true;
        pieceSelected();
    }

    private void placeTank()
    {
        Destroy(rocketParts_displayed_one);
        Destroy(rocketParts_displayed_two);
        Destroy(rocketParts_displayed_three); 
        
        rocketParts_displayed_one = Instantiate(carburant[0], new Vector3(6, 4.5f, -1.2f), Quaternion.Euler(0f, 0f, 0f));
        rocketParts_displayed_two = Instantiate(carburant[1], new Vector3(6, 4.5f, 0), Quaternion.Euler(0f, 0f, 0f));
        rocketParts_displayed_three = Instantiate(carburant[2], new Vector3(6, 4.5f, 1.2f), Quaternion.Euler(0f, 0f, 0f));

        isTankTurn = false;
        isEngineTurn = true;
    }

    private void placeEngine()
    {
        Destroy(rocketParts_displayed_one);
        Destroy(rocketParts_displayed_two);
        Destroy(rocketParts_displayed_three);

        rocketParts_displayed_one = Instantiate(moteur[0], new Vector3(6, 4.7f, -1.2f), Quaternion.Euler(0f, 0f, 0f));
        rocketParts_displayed_two = Instantiate(moteur[1], new Vector3(6, 4.7f, 0), Quaternion.Euler(0f, 0f, 0f));
        rocketParts_displayed_three = Instantiate(moteur[2], new Vector3(6, 4.7f, 1.2f), Quaternion.Euler(0f, 0f, 0f));
        
        isEngineTurn = false;
    }
}
