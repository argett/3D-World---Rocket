using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    private float speed = 100;
    public GameObject halo;
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
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    halo.transform.position = hit.collider.gameObject.transform.position;
                    halo.SetActive(true);
                }
            }
            else
                halo.SetActive(false);

        }
    }
}
