using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject player;

    private GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void load_rocket_lab()
    {
        SceneManager.LoadScene("Rocket lab");
        Instantiate(player, new Vector3(0, 1, -2), Quaternion.identity);
    }

    public void load_lunch_pad()
    {
        SceneManager.LoadScene("Lunch pad");
        Instantiate(player, new Vector3(0, 1, 2), Quaternion.identity);
        Instantiate(rocket, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
