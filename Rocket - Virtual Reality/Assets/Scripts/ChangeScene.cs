using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    private GameObject player;
    private GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void load_lunch_pad()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        rocket.transform.position = new Vector3(10, rocket.GetComponent<Rocket>().getHeigth()-1f, 0f);
        rocket.transform.parent = null;
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Launch pad", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObjects (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(rocket, SceneManager.GetSceneByName("Launch pad"));
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("Launch pad"));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
