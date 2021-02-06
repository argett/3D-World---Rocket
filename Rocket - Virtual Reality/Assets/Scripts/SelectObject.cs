using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject player;

    private float speed = 100;
    private GameObject halo = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] HALO = GameObject.FindGameObjectsWithTag("Halo");
        Debug.Log(HALO.Length);
        halo = HALO[0];
        halo.SetActive(false);
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
                }else if (hit.collider.gameObject.layer == 9){

                    hit.collider.transform.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
                }
            }
            else
                halo.SetActive(false);

        }
    }
}
