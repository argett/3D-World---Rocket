using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject dontDestroy;

    public GameObject player;
    public GameObject rocket;


    //private List<RocketPart> rocket;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)            //Check if instance already exists
            instance = this;
        else if (instance != this)
            Destroy(dontDestroy);         //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.


        Instantiate(player, new Vector3(0, 5.1f, 0), Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
