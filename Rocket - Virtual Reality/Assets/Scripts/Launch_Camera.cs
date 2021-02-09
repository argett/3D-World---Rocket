using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch_Camera : MonoBehaviour
{
    public Camera cam;
    public GameObject player, rocket;
    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectsWithTag("Rocket")[0];
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Creat_Cam()
    {
        Destroy(player);
        Instantiate(cam, new Vector3(rocket.transform.position.x, rocket.transform.position.y,rocket.transform.position.z - 10), Quaternion.identity, rocket.transform);
    }
}
