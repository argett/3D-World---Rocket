using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    private int actualScene = 0;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        actualScene = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (actualScene != 1)
            {
                SceneManager.LoadScene("Rocket lab");
                Instantiate(player, new Vector3(0, 1, -2), Quaternion.identity);
                actualScene = 1;
                Debug.Log("L");
            }
        }
        else if (Input.GetKey(KeyCode.M))
        { 
            if (actualScene != 2)
            {
                SceneManager.LoadScene("Lunch pad");
                Instantiate(player, new Vector3(0, 1, 2), Quaternion.identity);
                actualScene = 2;
                Debug.Log("M");
            }
        }
    }
}
