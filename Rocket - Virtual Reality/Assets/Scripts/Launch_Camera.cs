using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch_Camera : MonoBehaviour
{
    public Camera cam;//Camera prefab

    private GameObject player, rocket;
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Creat_Cam()
    {
        //We destroy the fpscontroller to avoid cam conflict
        Destroy(player);
        //We creat a new camera as son of the rocket to make it move with it
        Instantiate(cam, new Vector3(rocket.transform.position.x, rocket.transform.position.y,rocket.transform.position.z - 10), Quaternion.identity, rocket.transform);
        //We creat a light to make the rocket well illuminated
        GameObject lightGameObject = new GameObject("The Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.transform.parent = rocket.transform;
        lightComp.transform.localPosition = new Vector3(0, -3, -12);
        lightComp.range = 30;
        lightComp.intensity = 5;
        //We increase the farplane to make the planets visible (because there is only big spere to generate there is not too much performance needed)
        cam.farClipPlane = 100000;
    }
}
