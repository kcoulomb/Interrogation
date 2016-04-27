using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

    private string prev_scene = "hello";

    //move scenes based on scene_name
    public void moveScene(string scene_name)
    {
        Application.LoadLevel(scene_name);
    }

    public void setPrevScene(string previous_scene)
    {
        prev_scene = previous_scene;
        Debug.Log(prev_scene);
    }

    public string getPrevScene()
    {
        return prev_scene;
    }

}
