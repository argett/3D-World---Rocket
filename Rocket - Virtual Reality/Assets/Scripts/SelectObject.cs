using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    private float speed = 100;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int layerMask = 0 << 10;
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
                Debug.Log(hit.collider.gameObject.name);
            }
            else
                Debug.Log("rien");

        }
    }
}
