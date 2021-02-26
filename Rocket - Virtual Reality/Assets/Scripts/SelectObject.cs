using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectObject : MonoBehaviour
{
    public GameObject player;        // player prefab
    public GameObject displayText_prefab; // gameobject for text prefab
    public GameObject pistolPump;    // hand pumps prefab 
    public Material halo_white;      // halo prefab
    public LineRenderer laser;       // ray prefab

    private GameObject rocket_pad;   // gameobject rocket_pad that will contains all rocket parts and the pad
    private GameObject rocket;       // gameobject rocket that will contains only the rocket parts
    private GameObject displayText;  // text displaying rocket part Characteristics or fuel value
    private GameObject halo;         // halo that show that we can click on an object
    private GameObject hand;         // the player's hand
    private GameObject handPump;     // the pistol pump variable that will be on the piedestal & the player's hand
    private GameObject pumpPiedestal;// the parent gameobject of the pistol pump
    private GameObject objInHand;    // Color selected (doesnt take fuel pump into account)
    private GameObject colorPiedestal; // the parent of the color material 
    private LineRenderer ray;        // ray that show that we can take an object
    private bool handlingPump;       // to know if the pump is in the hand --> to trace a ray between the pump button and the hand 
    private bool handlingColor;      // to avoid to catch 2 things at the same time
    private bool justGrabbed;        // to know if an object has been selected in the frame, so to not drop it in the same frame

    // Start is called before the first frame update
    void Start()
    {
        hand = this.transform.Find("FirstPersonCharacter").gameObject.transform.Find("Hand").gameObject; // Hand is a child of this
        hand.GetComponent<Renderer>().material.color = UnityEngine.Color.gray;

        if (SceneManager.GetActiveScene().name == "Rocket lab")
        {
            pumpPiedestal = GameObject.FindGameObjectWithTag("Fuel_Piedestal");
            rocket_pad = GameObject.FindGameObjectWithTag("Rocket_pad");
            rocket = GameObject.FindGameObjectWithTag("Rocket");
            handPump = Instantiate(pistolPump, pumpPiedestal.transform);
        }

        objInHand = null;
        colorPiedestal = null;
        displayText = null;
        halo = null;
        ray = null;
        handlingPump = false;
        handlingColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        justGrabbed = false;

        if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
        {
            GameObject collided = hit.collider.gameObject;

            if (collided.layer == 11) // maybe Rocket part (the selectionnable ones)
            {
                destroyHalo();
                createHalo(collided);
                Thread.Sleep(10);       // to avoid the creation of mutliples halo's layer because the instantiation takes more than 1 frame    
            }
            else if (collided.layer == 9) //user interraction layer
            {
                switch (collided.tag)
                {
                    case "Button":
                        destroyHalo();
                        createHalo(collided);
                        break;

                    case "Paint_ball":
                        destroyRay();
                        traceRay(collided, "hand");
                        if (Input.GetMouseButtonDown(0))
                            grabColor(collided);

                        break;

                    case "Fuel_Output":
                        if (Input.GetMouseButtonDown(0) && handlingPump)
                            linkFuelPipe(collided);
                        break;

                    case "Fuel_Input":
                        displayCharacteristics(collided, "fuel");
                        if (Input.GetMouseButton(0))
                            fillRocketTank();
                        break;

                    case "Fuel_Pistol":
                        destroyRay();
                        traceRay(collided, "hand");
                        if (Input.GetMouseButtonDown(0))
                            grabFuelPump(collided);
                        break;

                    default:
                        Debug.Log("ERROR : gameObject tag for layer 9 not correct");
                        break;
                }
            }
            else if (collided.layer == 10) // halo for selectionnable object
            {
                switch (collided.tag)
                {
                    case "Button":
                        if (Input.GetMouseButtonDown(0))
                            checkButtonType(collided);
                        break;
                    default:
                        // rocket part tag (they might have each one a different tag so i put this in the default case)
                        displayCharacteristics(collided, "rocketPart");
                        if (Input.GetMouseButtonDown(0))
                            choseObject(collided.transform.parent.gameObject);
                        break;
                }
            }
            else if (collided.layer == 8 || collided.layer == 12)  // rocket part (crew/tank) or reactors
            { 
                if(handlingColor && Input.GetMouseButtonDown(0))
                    throwColorBall(collided);
            }
            else
            {
                destroyAllInstance(); 
                if (handlingColor && !handlingPump)
                {
                    if (Input.GetMouseButtonDown(0) && !justGrabbed)
                        dropColorBall(false);
                }
            }
        }
        else
        {
            destroyAllInstance();
            if (handlingColor && !handlingPump)
            {
                if (Input.GetMouseButtonDown(0) && !justGrabbed)
                    dropColorBall(false);
            }
        }
    }

    private void displayCharacteristics(GameObject obj, string where)
    {
        destroyText();

        Vector3 parentPos = obj.transform.position;
        // make the text face the player
        displayText = Instantiate(displayText_prefab, new Vector3(parentPos.x, parentPos.y, parentPos.z), Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f));
        // the text is in the rocket part, we translate it to the right
        displayText.transform.Translate(new Vector3(0, 1, 0));

        string text = "";
        switch (where)
        {
            case "fuel":
                // display the amuont of fuel transfered
                text += "Fuel transfered :\n";
                text += rocket.GetComponent<Rocket>().getFuelTransfered().ToString();
                text += " /100";
                break;
            case "rocketPart":
                // display the rocket part characteristics
                Characteristics rocketPart_info = obj.GetComponent<Characteristics>();
                int fuel = rocketPart_info.fuel;
                int weight = rocketPart_info.weight;
                int power = rocketPart_info.power;
                int crew = rocketPart_info.crew;

                if (fuel != 0)
                    text += "Total amount of fuel = " + fuel + "\n";
                if (weight != 0)
                    text += "Total weight of the part = " + weight + "\n";
                if (power != 0)
                    text += "Total power in Newton = " + power + "\n";
                if (crew != 0)
                    text += "Maximum crew = " + crew + "\n";
                break;
        }

        displayText.GetComponent<TextMesh>().text = text;
    }

    private void choseObject(GameObject obj)
    {
        rocket_pad.GetComponent<CreateRocket>().placeObject(obj);
        // change the items
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PresentRocketParts>().pieceSelected();
    }

    private void checkButtonType(GameObject obj)
    {
        switch (obj.name)
        {
            case "Add_stage":
                obj.gameObject.GetComponent<Buttons>().Add_stage();
                break;
            case "Goto_lunch_pad":
                obj.gameObject.GetComponent<Buttons>().Goto_lunch_pad();
                break;
            case "Launch_button":
                obj.gameObject.GetComponent<Buttons>().Decollage(obj.gameObject);
                break;
            default:
                Debug.Log("ERROR : button name not correct");
                break;
        }
    }

    private void destroyCharacteristics()
    {
        if (displayText != null)
        {
            Destroy(displayText);
            displayText = null;
        }
    }

    private void createHalo(GameObject obj)
    {
        halo = Instantiate(obj, obj.transform.position, Quaternion.identity, obj.transform);
        halo.layer = 10;
        halo.tag = obj.tag;
        halo.name = obj.name;
        halo.transform.localScale = new Vector3(1.1f, 1f, 1.1f);
        halo.GetComponent<Renderer>().material = halo_white;
    }

    private void destroyHalo()
    {
        if (halo != null)
        {
            Destroy(halo);
            halo = null;
        }
    }

    private void grabColor(GameObject obj)
    {
        if (!handlingColor && !handlingPump)
        {
            handlingColor = true;
            justGrabbed = true;
            colorPiedestal = obj.transform.parent.transform.gameObject;
            obj.transform.SetParent(hand.transform);
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.transform.localPosition = new Vector3(0.75f, 0, 1.2f);
            objInHand = obj;
        }
    }

    private void throwColorBall(GameObject rocketPart)
    {
        rocketPart.GetComponent<Renderer>().material = objInHand.GetComponent<Renderer>().material;
        dropColorBall(true);
    }

    private void dropColorBall(bool resetPos)
    {
        handlingColor = false;
        objInHand.transform.parent = colorPiedestal.transform;

        if (resetPos)
            objInHand.transform.localPosition = new Vector3(0, 1.5f, 0);

        objInHand.GetComponent<Rigidbody>().isKinematic = false;
        colorPiedestal = null;
        objInHand = null;
    }

    private void grabFuelPump(GameObject pump)
    {
        if (!handlingColor)
        {
            handPump.transform.parent = hand.transform;
            handPump.transform.localPosition = new Vector3(0.275f, 0, 1.2f);
            handPump.transform.localScale = new Vector3(1.4f, 1.3f, 1.3f);
            handPump.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
            handlingPump = true;
            handlingColor = true;
        }
    }

    private void linkFuelPipe(GameObject fuelOutput)
    {
        // create a line between the output fuel and the pistol pump in the hand like a pipe linking both sides
        StartCoroutine(pipeFollowPlayer(fuelOutput));
    }

    private IEnumerator pipeFollowPlayer(GameObject outputFuel)
    {
        while (handlingPump)
        {
            destroyRay();
            traceRay(outputFuel, "pump");
            yield return null;
        }
    }

    private void placePistolPumpOnPiedestal()
    {
        handPump.transform.parent = pumpPiedestal.transform;
        handPump.transform.localPosition = new Vector3(0, 1.2f, 0);
        handPump.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        handPump.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        handlingPump = false;
        handlingColor = false;
    }

    private void fillRocketTank()
    {
        if (handlingPump && rocket.GetComponent<Rocket>().getFuelTransfered() < 100)
        {
            rocket.GetComponent<Rocket>().addFuelTransfered();
        }
        else if(rocket.GetComponent<Rocket>().getFuelTransfered() >= 100)
        {
            destroyRay();
            placePistolPumpOnPiedestal();
        }
    }

    private void destroyRay()
    {
        if (ray != null)
        {
            Destroy(ray.gameObject);
            ray = null;
        }
    }

    private void traceRay(GameObject point, string from)
    {
        ray = Instantiate(laser);
        ray.positionCount = 2;
        if(from == "hand")
            ray.SetPosition(0, hand.transform.position);
        else if(from == "pump")
            ray.SetPosition(0, handPump.transform.position);

        ray.SetPosition(1, point.transform.position);
    }

    private void destroyText()
    {
        if(displayText != null)
        {
            Destroy(displayText.gameObject);
            displayText = null;
        }
    }

    private void destroyAllInstance()
    {
        destroyText();
        destroyRay();
        destroyHalo();
        destroyCharacteristics();
    }
}
