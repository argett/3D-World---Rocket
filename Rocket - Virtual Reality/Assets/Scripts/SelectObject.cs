using System.Threading;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject player;
    public GameObject caracteristic_rocket_part_prefab;
    public Material halo_white;

    private GameObject rocket_build;
    private GameObject caracteristic_rocket_part;
    private GameObject halo = null;
    private GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        hand = this.transform.Find("FirstPersonCharacter").gameObject.transform.Find("Hand").gameObject; // Hand is a child of this
        rocket_build = GameObject.FindGameObjectsWithTag("Rocket_pad")[0];
        caracteristic_rocket_part = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        hand.GetComponent<Renderer>().material.color = UnityEngine.Color.gray;

        if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
        {
            GameObject collided = hit.collider.gameObject;
            if (collided.layer == 11) // maybe Rocket part (the selectionnable ones)
            {
                destroyHalo();
                createHalo(collided);
                Thread.Sleep(10);       // to avoid the creation of mutliples halo's layer because the instantiation takes more than 1 frame    
            }
            else if (collided.layer == 9) //user interrqction : layer button, UI, winglet
            {
                if (collided.tag == "Button")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        checkButtonType(hit.collider);
                    }
                }
                else if(collided.tag == "Winglet")
                {
                    hand.GetComponent<Renderer>().material.color = UnityEngine.Color.red;

                    if (Input.GetMouseButtonDown(0))
                    {
                        grabObject();
                    }
                }
            }
            else if(collided.layer == 10) // halo for selectionnable object
            {
                if (Input.GetMouseButtonDown(0))
                {
                    choseObject(hit.collider.transform.parent.gameObject);
                }

                displayCaracteristics(hit.collider);
            }
            else
            {
                destroyHalo();
                destroyCaracteristics();
            }
        }
        else
        {
            destroyHalo();
            destroyCaracteristics();
        }
    }

    private void displayCaracteristics(Collider obj)
    {
        if(caracteristic_rocket_part == null)
        {
            Vector3 parentPos = obj.gameObject.transform.position;
            // make the text face the player
            caracteristic_rocket_part = Instantiate(caracteristic_rocket_part_prefab, new Vector3(parentPos.x, parentPos.y, parentPos.z), Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f));
            // the text is in the rocket part, we translate it to the right
            caracteristic_rocket_part.transform.Translate(new Vector3(1f, 0, 0));
        }

        Caracteristics rocketPart_info = obj.GetComponent<Caracteristics>();
        int fuel = rocketPart_info.fuel;
        int weight = rocketPart_info.weight;
        int power = rocketPart_info.power;
        int crew = rocketPart_info.crew;

        string caract = "";
        if (fuel != 0)
            caract += "Total amount of fuel = " + fuel + "\n";
        if (weight != 0)
            caract += "Total weight of the part = " + weight + "\n";
        if (power != 0)
            caract += "Total power in Newton = " + power + "\n";
        if (crew != 0)
            caract += "Maximum crew = " + crew + "\n";

        caracteristic_rocket_part.GetComponent<TextMesh>().text = caract;
    }

    private void choseObject(GameObject obj)
    {
        rocket_build.GetComponent<CreateRocket>().placeObject(obj);
        // change the items
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PresentRocketParts>().pieceSelected();
    }

    private void checkButtonType(Collider obj)
    {
        switch (obj.name)
        {
            case "Add_stage":
                obj.gameObject.GetComponent<Buttons>().Add_stage();
                break;
            case "Goto_lunch_pad":
                obj.gameObject.GetComponent<Buttons>().Goto_lunch_pad();
                break;
            case "Place_winglet":
                obj.gameObject.GetComponent<Buttons>().Place_winglet();
                break;
        }
    }

    private void destroyCaracteristics()
    {
        if (caracteristic_rocket_part != null)
        {
            Destroy(caracteristic_rocket_part);
            caracteristic_rocket_part = null;
        }
    }

    private void destroyHalo()
    {
        if (halo != null)
        {
            Destroy(halo);
            halo = null;
        }
    }

    private void createHalo(GameObject rocketPart)
    {
        halo = Instantiate(rocketPart, rocketPart.transform.position, Quaternion.identity, rocketPart.transform);
        halo.layer = 10;
        halo.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        halo.GetComponent<Renderer>().material = halo_white;
    }

    private void grabObject()
    {
        GameObject winglet = GameObject.FindGameObjectWithTag("Winglet");
        winglet.transform.SetParent(hand.transform);
        winglet.GetComponent<Rigidbody>().isKinematic = true;
        winglet.transform.localPosition = new Vector3(0.75f, 0, 1.2f);
    }
}
